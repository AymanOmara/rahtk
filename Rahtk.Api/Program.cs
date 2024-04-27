using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Rahtk.IOC;
using Rahtk.Shared;
using Rahtk.Api.Utils;
using Rahtk.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterIOCServices(builder.Configuration);
builder.Services.SetUpApiServices(builder.Configuration);


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.OperationFilter<AddRequiredHeaderParameter>();
});

builder.Services
    .AddControllers()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseLocalization();
app.UseHttpsRedirection();
app.UseShared();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

