using GrpcServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddGrpc(options =>
{
    options.MaxReceiveMessageSize = 25 * 1024 * 1024;
    options.MaxSendMessageSize = 25 * 1024 * 1024;
    // options.ResponseCompressionAlgorithm = "gzip";
    // options.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Fastest;
    //  options.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<StackOverflowService>();
app.MapControllers();
// app.UseMiddleware<TrafficLoggerMiddleware>();
app.Run();
