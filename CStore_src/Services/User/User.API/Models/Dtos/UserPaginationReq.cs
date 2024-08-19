namespace User.API.Models.Dtos
{
    public record UserPaginationReq
    (
        string? Email,
        string? AuthMethod, 
        string Field = "id",     
        string Order = "asc",   
        int Limit = 5,    
        int Page = 1
    );
}
