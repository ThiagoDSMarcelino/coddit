using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Securitas.JWT;
using Securitas;
using System.Text;

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

builder.Services.AddTransient<EnvironmentFile>(
    p => new EnvironmentFile(".env")
);

builder.Services.AddTransient<IPasswordProvider>(
    p => new ConstPasswordProvider(
        p.GetService<EnvironmentFile>()
            .Get("SECRET_PASSWORD")
    )
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
builder.Services.AddTransient<IRepository<User, long>, UserRepository>();

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();