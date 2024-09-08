namespace User.API.Models.Dtos.UserDtos
{
    public record UserLoginReq
    (
        string Email,
        string Password
    );
}
