
namespace User.API.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(CreateUserDto user);
        Task<UserLoginRes> Signin(UserLoginReq user);
        Task<UserLoginRes> SigninSocialMedia(SocialMediaReq social);
        Task<PaginatedResult<ListUserDto>> GetUserPagination(UserPaginationReq request);
        Task<bool> DeleteUser(Guid id);
        Task<bool> UpdateUser(UpdateUserDto userDto);
    }
}


