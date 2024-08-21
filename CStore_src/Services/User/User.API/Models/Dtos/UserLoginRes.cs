namespace User.API.Models.Dtos
{
    public record UserLoginRes
    (
        string  Access_token,
        string  Refresh_token,
        UserNoPasswordDto? User
    );
}
