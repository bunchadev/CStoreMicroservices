using User.API.Models.Dtos.UserDtos;

namespace User.API.Users.UpdateUser
{
    public record UpdateUserCommand(
        Guid UserId,
        string Email,
        string Password,
        bool IsActive,
        Guid RoleId
    ) : ICommand<UpdateUserResult>;
    public record UpdateUserResult(bool Status);
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
               .NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required")
               .EmailAddress().WithMessage("Email must be a valid email address");
            RuleFor(x => x.IsActive)
               .NotEmpty().WithMessage("IsActive is required");
            RuleFor(x => x.RoleId)
               .NotEmpty().WithMessage("RoleId is required");
        }
    }
    internal class UpdateUserHandler(IUserRepository userRepository)
        : ICommandHandler<UpdateUserCommand, UpdateUserResult>
    {
        public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var data = request.Adapt<UpdateUserDto>();
            var result = await userRepository.UpdateUser(data);
            return new UpdateUserResult(result);
        }
    }
}

