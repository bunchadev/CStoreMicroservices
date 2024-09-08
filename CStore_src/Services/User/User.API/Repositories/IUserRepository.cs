
using User.API.Models.Dtos.UserDtos;

namespace User.API.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(CreateUserDto user);
        Task<UserLoginRes> Signin(UserLoginReq user);
        Task<UserLoginRes> SigninSocialMedia(SocialMediaReq social);
        Task<UserLoginRes> RefreshToken(Guid id,string token);
        Task<PaginatedResult<ListUserDto>> GetUserPagination(UserPaginationReq request);
        Task<bool> DeleteUser(Guid id);
        Task<bool> UpdateUser(UpdateUserDto userDto);
    }
}


