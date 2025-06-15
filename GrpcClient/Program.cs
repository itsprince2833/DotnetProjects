using Grpc.Net.Compression;
using GrpcServer;
using StackOverflow;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// configure http client for rest
builder.Services.AddHttpClient("stackoverflow", c =>
{
    c.BaseAddress = new Uri("https://localhost:7125");
});

builder.Services.AddGrpcClient<Greeter.GreeterClient>(o =>
{
    o.Address = new Uri("https://localhost:7125");
}).ConfigureChannel(c =>
{
    c.MaxReceiveMessageSize = 25 * 1024 * 1024;
    c.MaxSendMessageSize = 25 * 1024 * 1024;
});

builder.Services.AddGrpcClient<Stackoverflow.StackoverflowClient>(o =>
{
    o.Address = new Uri("https://localhost:7125");
}).ConfigureChannel(c =>
{
    c.MaxReceiveMessageSize = 25 * 1024 * 1024;
    c.MaxSendMessageSize = 25 * 1024 * 1024;
});;

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
// app.UseHttpsRedirection();
app.Run();