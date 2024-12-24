using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Infraestrutura;
using WebApi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
    {
      new OpenApiSecurityScheme {
        Reference = new OpenApiReference {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer",
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,
      },
      new List<string>()
    }
  });
});

builder.Services.AddTransient<IColaboradoresRepository, ColaboradoresRepository>();

var key = Encoding.ASCII.GetBytes(WebApi.Key.Secret);

builder.Services.AddAuthentication(x =>
{
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
  x.RequireHttpsMetadata = false;
  x.SaveToken = true;
  x.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false
  };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
    c.RoutePrefix = string.Empty;
  });

  app.UseExceptionHandler("/error-development");
} else {
  app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();