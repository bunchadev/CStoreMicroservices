namespace User.API.Models.Dtos
{
    public record UserNoPasswordDto
    (
        Guid UserId,
        string Email,
        string AuthMethod,
        bool IsActive,
        string RoleName
    );
}
