using Microsoft.Extensions.Caching.Distributed;

namespace User.API.Repositories
{
    public class CachedUserRepository
        (IUserRepository userRepository, IDistributedCache cache)
        : IUserRepository
    {
        public async Task<bool> CreateUser(CreateUserDto user)
        {
            return await userRepository.CreateUser(user);
        }

        public Task<List<UserDto>> GetUserPagination()
        {
            throw new NotImplementedException();
        }

        public async Task<UserLoginRes> Signin(UserLoginReq user)
        {
            return await userRepository.Signin(user);
        }

        public async Task<UserLoginRes> SigninSocialMedia(SocialMediaReq social)
        {
            return await userRepository.SigninSocialMedia(social);
        }
    }
}
