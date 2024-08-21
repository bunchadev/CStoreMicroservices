namespace User.API.Models.Dtos
{
    public record ListUserDto
    (
        Guid UserId,
        string Email,
        string Password,
        string AuthMethod,
        bool IsActive,
        DateTime UpdatedAt,
        string RoleName
    );
}
