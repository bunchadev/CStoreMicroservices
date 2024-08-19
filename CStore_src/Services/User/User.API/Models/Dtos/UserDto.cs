namespace User.API.Models.Dtos
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
