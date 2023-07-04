using Coddit.Repositories.MemberReposiory;
using Coddit.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Securitas;
using Securitas.JWT;
using System.Security.Cryptography;
using System.Text;

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

builder.Services.AddSingleton(
    p => new EnvironmentFile("../.env")
);

builder.Services.AddScoped<HashAlgorithm>(
    p => SHA256.Create()
);

builder.Services.AddSingleton(
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
builder.Services.AddTransient<IRepository<Role>, RoleRepository>();
builder.Services.AddTransient<IRepository<Forum>, ForumRepository>();
builder.Services.AddTransient<IMemberRepository, MemberRepository>();

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