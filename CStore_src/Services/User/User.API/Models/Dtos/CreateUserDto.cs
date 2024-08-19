namespace User.API.Models.Dtos
{
    public record CreateUserDto
    (
        string Email,
        string Password,
        string Auth,
        string Role
    );
}
