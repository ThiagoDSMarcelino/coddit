using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Securitas.JWT;
using System.Text;
using Backend;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MainPolicy",
    policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

EnvironmentVariable variable = new EnvironmentVariable(".env");
var secretKey = variable.Get<string>("SECRET_PASSWORD");

builder.Services.AddTransient<IPasswordProvider>(
    p => new ConstPasswordProvider(secretKey)
);

builder.Services.AddTransient<Encoding>(
    p => Encoding.UTF8
);

builder.Services.AddTransient<IJWTService>(
    p => new JWTService(
        p.GetService<IPasswordProvider>()!,
        p.GetService<Encoding>()!,
        HashAlgorithmType.HS256
    )
);

builder.Services.AddScoped<CodditContext>();
builder.Services.AddTransient<IRepository<User>, UserRepository>();

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();