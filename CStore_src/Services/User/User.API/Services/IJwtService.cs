namespace User.API.Services
{
    public interface IJwtService
    {
        string GenerateToken(Guid id, string role, int hours);
    }
}
