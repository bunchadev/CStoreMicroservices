﻿using User.API.Models.Dtos.UserDtos;

namespace User.API.Users.RefreshToken
{
    public record RefreshTokenCommand(Guid Id,string Token) : ICommand<UserLoginRes>;
    internal class RefreshTokenHandler
        (IUserRepository userRepository)
        : ICommandHandler<RefreshTokenCommand, UserLoginRes>
    {
        public async Task<UserLoginRes> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.RefreshToken(request.Id,request.Token);
            return user;
        }
    }
}

