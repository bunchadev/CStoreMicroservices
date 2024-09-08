namespace User.API.Models.Dtos.UserDtos
{
    public record CreateUserDto
    (
        string Email,
        string Password,
        string Auth,
        string Role
    );
}
