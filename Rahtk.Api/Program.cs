using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Rahtk.IOC;
using Rahtk.Shared;
using Rahtk.Api.Utils;
using Rahtk.Api;
using Swashbuckle.AspNetCore.SwaggerGen;

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
    //options.OperationFilter<FileUploadOperationFilter>();
});

builder.Services
    .AddControllers()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseLocalization();
app.UseHttpsRedirection();
app.UseShared();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileUploadMime = "multipart/form-data";
        if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x => x.Key.Equals(fileUploadMime, System.StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        operation.RequestBody = new OpenApiRequestBody
        {
            Content = {
                [fileUploadMime] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties =
                        {
                            ["file"] = new OpenApiSchema
                            {
                                Type = "string",
                                Format = "binary"
                            }
                        },
                        Required = new HashSet<string> { "file" }
                    }
                }
            }
        };
    }
}