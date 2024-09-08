namespace User.API.Models.Dtos.UserDtos
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
