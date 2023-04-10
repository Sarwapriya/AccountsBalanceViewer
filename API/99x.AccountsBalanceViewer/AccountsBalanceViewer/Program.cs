using AccountsBalanceViewer.Application;
using AccountsBalanceViewer.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_allowSpecificOrigins";

var clientId = builder.Configuration["SwaggerOauthConfiguration:FrontEndAppClientId"];
var clientSecret = builder.Configuration["SwaggerOauthConfiguration:FrontEndAppClientSecret"];

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
        policy =>
        {

            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddDbContext<AccountsBalanceViewerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AccBalanceViewerConnectionString")));

builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri($"{builder.Configuration["SwaggerOauthConfiguration:AuthEndpoint"]}"),
                TokenUrl = new Uri($"{builder.Configuration["SwaggerOauthConfiguration:TokenEndpoint"]}"),
                Scopes = new Dictionary<string, string>
                {
                    { $"{builder.Configuration["SwaggerOauthConfiguration:Scopes"]}", "default" }
                }
            }
        }
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
        {
            //s.OAuthClientId(clientId);
            //s.OAuthClientSecret(clientSecret);
            //s.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AccountsBalanceViewerContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(AllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
