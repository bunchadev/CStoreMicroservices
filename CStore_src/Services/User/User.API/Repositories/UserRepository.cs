using Dapper;
using System.Data;
using User.API.Data;
using User.API.Models.Dtos.UserDtos;
using User.API.Services;

namespace User.API.Repositories
{
    public class UserRepository(
        ISqlConnectionFactory connectionFactory, 
        IRoleRepository role,
        ITokenRepository tokenRepository,
        IPasswordService password,
        IJwtService jwtService
    ) : IUserRepository
    {
        public async Task<bool> CreateUser(CreateUserDto user)
        {
            Guid? roleId = await role.GetRoleIdWithName(user.Role);
            if (roleId is null) {
                return false;
            }
            return await Save(user, roleId.ToString()); 
        }
        private async Task<bool> Save(CreateUserDto user,string? roleId)
        {
            using var connection = connectionFactory.Create();
            connection.Open();
            using var transaction = connection.BeginTransaction(); 
            try
            {
                 var parameters1 = new { 
                     Email = user.Email, 
                     Password = password.HashPassword(user.Password),
                     AuthMethod = user.Auth,
                     Role = roleId
                 };
                await connection.ExecuteAsync("dbo.InsertUser", parameters1, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                return true;
            }
            catch (Exception) 
            {
                 transaction.Rollback();
                 return false;
            }
        }
        private async Task<UserDto?> GetUserWithRole(string email)
        {
            string query = """
                SELECT u.user_id as UserId,
                       u.email as Email,
                       u.password as Password,
                       u.auth_method as AuthMethod,
                       u.is_active as IsActive,
                       r.role_name as RoleName
                FROM users u 
                INNER JOIN roles r ON u.role_id = r.role_id
                WHERE u.email = @Email
            """;
            using var connection = connectionFactory.Create();
            return await connection.QueryFirstOrDefaultAsync<UserDto?>(
                query,
                new { Email = email }
            );
        }
        public async Task<UserLoginRes> Signin(UserLoginReq user)
        {
            var userRole = await GetUserWithRole(user.Email);
            if (userRole is null)  return new UserLoginRes("", "",0,null);
            if (password.VerifyPassword(userRole.Password, user.Password) == false) return new UserLoginRes("", "",0, null);
            if (userRole.IsActive == false) return new UserLoginRes("", "",0,null);
            var refresh_token = jwtService.GenerateToken(userRole.UserId, userRole.RoleName, 60);
            var access_token = jwtService.GenerateToken(userRole.UserId, userRole.RoleName, 5);
            var check = await tokenRepository.CreateRefreshToken(userRole.UserId, refresh_token);
            if (check == false) return new UserLoginRes("", "", 0, null);
            return new UserLoginRes(
                   access_token,
                   refresh_token,
                   300,
                   new UserNoPasswordDto(
                       userRole.UserId,
                       userRole.Email,
                       userRole.AuthMethod,
                       userRole.IsActive,
                       userRole.RoleName
                  )
            );
        }
        public async Task<UserLoginRes> SigninSocialMedia(SocialMediaReq social)
        {
            var user = await GetUserWithRole(social.Email);
            if (user is null)
            {
                Guid? roleId = await role.GetRoleIdWithName("User");
                if (roleId is null) 
                   return new UserLoginRes("","",0,null);
                CreateUserDto data = new CreateUserDto
                (
                     social.Email,
                     "2003",
                     social.Auth,
                     "User"
                );
                var check = await Save(data, roleId.ToString());
                if (check == false) 
                   return new UserLoginRes("", "",0,null);
                user = await GetUserWithRole(social.Email);
            }
            if (user!.IsActive == false) 
               return new UserLoginRes("", "",0,null);
            if (password.VerifyPassword(user.Password, "2003") == false) 
               return new UserLoginRes("", "",0,null);
            var refresh_token = jwtService.GenerateToken(user.UserId, user.RoleName, 60);
            var access_token = jwtService.GenerateToken(user.UserId, user.RoleName, 5);
            var checkToken = await tokenRepository.CreateRefreshToken(user.UserId, refresh_token);
            if (checkToken == false) return new UserLoginRes("", "", 0, null);
            return new UserLoginRes(
                 access_token,
                 refresh_token,
                 300,
                 new UserNoPasswordDto(
                     user.UserId,
                     user.Email,
                     user.AuthMethod,
                     user.IsActive,
                     user.RoleName
                 )
            );
        }

        public async Task<PaginatedResult<ListUserDto>> GetUserPagination(UserPaginationReq request)
        {
            var parameters = new
            {
                p_email = request.Email,
                p_auth_method = request.AuthMethod,
                p_field = request.Field,
                p_order = request.Order,
                p_page = request.Page,
                p_limit = request.Limit,
            };
            using var connection = connectionFactory.Create();
            var listUser = await connection.QueryAsync<ListUserDto>(
                    "dbo.PaginateUsers",
                     parameters,
                     commandType: CommandType.StoredProcedure
            );
            return new PaginatedResult<ListUserDto>(
                request.Page,
                request.Limit,
                listUser.Count(),
                listUser
            );
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            using var connection = connectionFactory.Create();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                var parameters = new
                {
                    UserId = id,
                };
                await connection.ExecuteAsync("dbo.DeleteUserById", parameters, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }

        public async Task<bool> UpdateUser(UpdateUserDto userDto)
        {
            Guid? roleId = await role.GetRoleWithId(userDto.RoleId);
            if (roleId is null)
               return false;
            using var connection = connectionFactory.Create();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                var parameters = new
                {
                    p_user_id = userDto.UserId,
                    p_email = (object)userDto.Email ?? DBNull.Value,
                    p_password = (object)password.HashPassword(userDto.Password) ?? DBNull.Value,
                    p_is_active = (object)userDto.IsActive ?? DBNull.Value,
                    p_role_id = (object)roleId ?? DBNull.Value
                };
                await connection.ExecuteAsync("dbo.UpdateUser", parameters, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }

        public async Task<UserLoginRes> RefreshToken(Guid id,string token)
        {
            var user = await GetUserById(id);
            if (user is null) return new UserLoginRes("", "", 0, null);
            var check_1 = await tokenRepository.CheckTokenByUserId(id, token);
            if (check_1 == false) return new UserLoginRes("", "", 0, null);
            var refresh_token = jwtService.GenerateToken(user.UserId, user.RoleName, 60);
            var access_token = jwtService.GenerateToken(user.UserId, user.RoleName, 5);
            var check_2 = await tokenRepository.CreateRefreshToken(id, refresh_token);
            if (check_2 == false) return new UserLoginRes("", "", 0, null);
            return new UserLoginRes(
                access_token,
                refresh_token,
                300,
                new UserNoPasswordDto(
                   user.UserId,
                   user.Email,
                   user.AuthMethod,
                   user.IsActive,
                   user.RoleName
                )
           );
        }
        public async Task<UserDto?> GetUserById(Guid id)
        {
            string query = """
                SELECT u.user_id as UserId,
                       u.email as Email,
                       u.password as Password,
                       u.auth_method as AuthMethod,
                       u.is_active as IsActive,
                       r.role_name as RoleName
                FROM users u 
                INNER JOIN roles r ON u.role_id = r.role_id
                WHERE u.user_id = @Id AND is_active = 1
            """;
            using var connection = connectionFactory.Create();
            return await connection.QueryFirstOrDefaultAsync<UserDto?>(
                query,
                new { Id = id }
            );
        }
    }
}
