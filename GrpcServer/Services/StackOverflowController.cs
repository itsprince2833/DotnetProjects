using GrpcServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Services;

public class UserDto
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = null!;
    public int Reputation { get; set; }
}

[ApiController]
[Route("[controller]")]
public class StackOverflowController : ControllerBase
{
    [HttpGet]
    [Route("GetAllUsers/{count:int}")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers(int count)
    {
        List<UserDto> users = new List<UserDto>();
        using (var context = new StackOverflow2010Context())
        {
            users = await context.Users.
                Select(u => new UserDto
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    Reputation = u.Reputation
                })
                .Take(count)
                .ToListAsync();
        }
        return Ok(users);
    }
}