using Zamzam.Application.Extentions;
using Zamzam.EF.Extensions;
using Zamzam.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddEfLayer(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
