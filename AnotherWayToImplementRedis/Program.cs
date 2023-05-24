using AnotherWayToImplementRedis.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Redis();
builder.Services.DependencyInjection();
builder.Services.ConfigureJson();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapHealthChecks("/healthcheck");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
