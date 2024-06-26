using FastFoodManagement.Infrastructure.Persistance;
using FastFoodEmployeeManagement.Infrastructure.IoC;
using FastFoodEmployeeManagement.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddMemoryCache();
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();



app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.Run();
