using Grpc.Core;
using GrpcServer.Models;
using Microsoft.EntityFrameworkCore;
using StackOverflow;
namespace GrpcServer.Services;
public class StackOverflowService: Stackoverflow.StackoverflowBase
{
    public override async Task<GetAllUsersResponse> GetAllUsers(GetAllUsersRequest request, ServerCallContext context)
    {
        List<StackOverflow.User> users;
        GetAllUsersResponse response = new GetAllUsersResponse();
        using (StackOverflow2010Context dbContext = new StackOverflow2010Context())
        {
            users = await dbContext.Users.Take(request.Count).
                    Select(u => new StackOverflow.User()
                    {
                        Id = u.Id,
                        DisplayName = u.DisplayName,
                        Reputation = u.Reputation
                    }).ToListAsync();
        }
        response.Users.AddRange(users);
        return response;
    }
}