using User.API.Models.Dtos.UserDtos;

namespace User.API.Users.GetListUser
{
    public class GetListUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/User/paginate", async ([AsParameters] UserPaginationReq request, ISender sender) =>
            {
                var query = request.Adapt<GetListUserQuery>();
                var response = await sender.Send(query);
                return Results.Ok(new Response<PaginatedResult<ListUserDto>>(
                        201,
                        "Get List User",
                        response
                ));
            }).RequireAuthorization("AdminOnly");


        }
    }
}

