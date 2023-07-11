using FoodCornerApi.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

//Register services (IoC container)
builder.Services.ConfigureServices(builder.Configuration);

//setup
var app = builder.Build();

//Configuration of middleware pipeline
app.ConfigureMiddlewarePipeline();

//setup
app.Run();