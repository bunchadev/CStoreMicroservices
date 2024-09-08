namespace User.API.Models.Dtos.UserDtos
{
    public record UpdateUserDto
    (
        Guid UserId,
        string Email,
        string Password,
        bool IsActive,
        Guid RoleId
    );
}
