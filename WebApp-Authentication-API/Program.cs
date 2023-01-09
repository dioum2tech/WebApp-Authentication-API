using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp_Authentication_API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add the Azure AD authentication
builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add versioning
builder.Services.AddCustomApiVersioning();

// Add configuration settings
builder.Services.AddOptions()
        .Configure<AzureAdOptions>(builder.Configuration.GetSection(ApiConfigurationSections.AzureAd));

// Add the Swagger generator
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseHttpsRedirection();

// Authenticate and authorize requests
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        //c.RoutePrefix = string.Empty;    
        //c.OAuthClientId("92407a47-20b0-4391-9740-db657ccf5fd4");
        //c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp_Authentication_API v1");

        c.OAuthClientId("92407a47-20b0-4391-9740-db657ccf5fd4");
        c.OAuthClientSecret("2278Q~IZgla6.4IcBkAmMis-kVrAzvM0AJlZgao5");
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });
}

app.MapControllers();

app.Run();
