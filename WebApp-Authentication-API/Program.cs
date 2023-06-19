using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp_Authentication_API.Infrastructure.Configuration;
using WebApp_Authentication_API.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

// Add the Azure AD authentication
builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Add versioning
builder.Services.AddCustomApiVersioning();

// Add the Swagger generator
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

// Add configuration settings
builder.Services.AddOptions()
        .Configure<AzureAdOptions>(builder.Configuration.GetSection(Constants.AzureAd));

var app = builder.Build();

var azureAD = app.Services.GetService<IOptions<AzureAdOptions>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthUsePkce();
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        c.OAuthClientId(azureAD.Value.SwaggerClientId);
        c.OAuthScopes("openid", azureAD.Value.CustomScopeApi);
    });
}

app.UseHttpsRedirection();

// Authenticate and authorize requests
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();