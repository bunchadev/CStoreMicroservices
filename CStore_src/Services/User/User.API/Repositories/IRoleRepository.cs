namespace User.API.Repositories
{
    public interface IRoleRepository
    {
        Task<Guid?> GetRoleIdWithName(string name);
    }
}
