namespace User.API.Models.Dtos.UserDtos
{
    public record UserDto
    (
        Guid UserId,
        string Email,
        string Password,
        string AuthMethod,
        bool IsActive,
        string RoleName
    );
}
