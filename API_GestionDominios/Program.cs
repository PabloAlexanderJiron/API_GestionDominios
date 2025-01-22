using API_GestionDominios.Middleware;
using API_GestionDominios.Servicios;
using Aplicacion.Helper.Servicios;
using Aplicacion;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using API_GestionDominios.DbSemilla;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using API_GestionDominios.CronJobs;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
builder.Services.AddCors(options =>
{
    options.AddPolicy("*",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IServicioUsuarioActual, ServicioUsuarioActual>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRETO_JWT")!))
    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gestion Dominios", Version = "v1" });
    // Definir el esquema de seguridad
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingrese el token JWT con el prefijo 'Bearer' (Ejemplo: 'Bearer 12345abcdef')",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Requerir el token en cada operaci�n
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AgregarAplicacion(builder.Configuration);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "7000"));
});
builder.Services.AddHostedService<EnviarEmailsDominiosPorCaducarCronJob>();
var meterProvider = Sdk.CreateMeterProviderBuilder()
    .AddPrometheusExporter()
    .AddMeter("Microsoft.AspNetCore.Hosting")
    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
    .AddMeter("Microsoft.AspNetCore.Http.Connections")
    .AddMeter("Microsoft.AspNetCore.Routing")
    .AddMeter("Microsoft.AspNetCore.Diagnostics")
    .AddMeter("Microsoft.AspNetCore.RateLimiting")
    .Build();
builder.Services.AddSingleton(meterProvider);
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.SemillaDataIncial();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExcepcionMiddleware>();
app.UseCors("*");
app.MapControllers();

app.Run();
