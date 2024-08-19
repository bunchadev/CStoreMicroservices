namespace User.API.Models.Dtos
{
    public record UserLoginReq
    (
        string Email,
        string Password
    );
}
