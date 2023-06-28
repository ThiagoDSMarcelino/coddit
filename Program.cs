using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Securitas.JWT;
using Securitas;
using System.Text;
using System.Security.Cryptography;
using Coddit.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MainPolicy",
    policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

#region Base services

builder.Services.AddTransient(
    p => new EnvironmentFile(".env")
);

builder.Services.AddTransient<HashAlgorithm>(
    p => SHA256.Create()
);

builder.Services.AddTransient(
    p => Encoding.UTF8
);

#endregion

#region Security services

builder.Services.AddTransient<IPasswordProvider>(
    p => new ConstPasswordProvider(
        p.GetService<EnvironmentFile>()
            .Get("SECRET_PASSWORD")
    )
);

builder.Services.AddTransient<ISecurityService>(
    p => new SecurityService(
        p.GetService<Encoding>(),
        p.GetService<HashAlgorithm>()
    )
);


builder.Services.AddTransient<IJWTService>(
    p => new JWTService(
        p.GetService<IPasswordProvider>()!,
        p.GetService<Encoding>()!,
        HashAlgorithmType.HS256
    )
);

#endregion

#region Repositories services

builder.Services.AddScoped<CodditContext>();
builder.Services.AddTransient<IRepository<User>, UserRepository>();

#endregion

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