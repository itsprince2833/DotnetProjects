using GrpcServer;
using Microsoft.AspNetCore.Mvc;
using StackOverflow;

namespace GrpcClient.Controllers;

[ApiController]
[Route("[controller]")]
public class GrpcClientController : ControllerBase
{
    private readonly Greeter.GreeterClient _client;
    private readonly Stackoverflow.StackoverflowClient _stackoverflowClient;
    private readonly ILogger<GrpcClientController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public GrpcClientController(ILogger<GrpcClientController> logger, Greeter.GreeterClient client, Stackoverflow.StackoverflowClient stackoverflow, IHttpClientFactory httpClientFactory)
    {
        _stackoverflowClient = stackoverflow;
        _httpClientFactory = httpClientFactory;
        _client = client;
        _logger = logger;
    }
    [HttpGet]
    [Route("GetGreeting")]
    public IActionResult GetGreeting()
    {
        _logger.LogInformation("Start GetGreeting");
        HelloReply reply = _client.SayHello(new HelloRequest { Name = "Prince" });
        _logger.LogInformation("End GetGreeting");
        return Ok(reply.Message);
    }

    [HttpGet]
    [Route("GetAllUsersViaGrpc/{count:int}")]
    public async Task<ActionResult<List<User>>> GetAllUsersViaGrpc(int count)
    {
        GetAllUsersResponse response = await _stackoverflowClient.GetAllUsersAsync(new GetAllUsersRequest { Count = count });
        return Ok(response.Users);
    }

    [HttpGet]
    [Route("GetAllUsersViaRest/{count:int}")]
    public async Task<ActionResult<List<User>>> GetAllUsersViaRest(int count)
    {
        HttpClient client = _httpClientFactory.CreateClient("stackoverflow");
        HttpResponseMessage response = await client.GetAsync($"/Stackoverflow/GetAllUsers/{count}");
        var users = await response.Content.ReadFromJsonAsync<List<User>>();
        return Ok(users);
    }
}