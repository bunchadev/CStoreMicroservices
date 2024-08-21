namespace User.API.Users.UpdateUser
{
    public record UpdateUserRequest(
        Guid UserId,
        string Email,
        string Password,
        bool IsActive,
        Guid RoleId
    );
    public class UpdateUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/v1/User/update", async (UpdateUserRequest request,ISender sender) =>
            {
                var command = request.Adapt<UpdateUserCommand>();
                var result = await sender.Send(command);
                if (result.Status is true)
                    return new Response<bool>(
                        201,
                        "Update success",
                        result.Status
                    );
                return new Response<bool>(
                        301,
                        "Update failed",
                        result.Status
                    );
            });
        }
    }
}

