namespace User.API.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(CreateUserDto user);
        Task<UserLoginRes> Signin(UserLoginReq user);
        Task<UserLoginRes> SigninSocialMedia(SocialMediaReq social);
        Task<List<UserDto>> GetUserPagination();
    }
}


