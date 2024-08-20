using System.Text;
using credo_bank.Application.Settings;
using credo_bank.Configuration;
using credo_bank.Infrastructure.DataContext;
using credo_bank.Middleware;
using credo_bank.Middleware.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.PeriodicBatching;

var builder = WebApplication.CreateBuilder(args);

var credoDbContextConnectionString = builder.Configuration.GetConnectionString("CredoBankDbContext");
var logConnetionString = builder.Configuration.GetConnectionString("LogsDbContext");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tera Auth Api", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});

builder.Services.AddServices();
builder.Services.AddConfigurations(builder.Configuration);

builder.Services.AddDbContext<CredoBankDbContext>(options => options.UseSqlServer(credoDbContextConnectionString));

var serviceProvider = builder.Services.BuildServiceProvider();
var efCoreSink = serviceProvider.GetRequiredService<EfCoreSink>();

var batchingOptions = new PeriodicBatchingSinkOptions
{
    BatchSizeLimit = 10,
    Period = TimeSpan.FromSeconds(5),
    EagerlyEmitFirstEvent = true
};

var batchedSink = new PeriodicBatchingSink(efCoreSink, batchingOptions);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.With(new CredentialsEnricher())
    .WriteTo.Async(wt => wt.Sink(batchedSink))
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Host.UseSerilog();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();