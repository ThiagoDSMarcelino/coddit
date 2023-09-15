using Securitas.JWT;
using System.Security.Cryptography;
using System.Text;

namespace Coddit.Services;

public class SecurityService : ISecurityService
{
    private readonly HashAlgorithm algorithm;
    private readonly Encoding encoding;
    private readonly IJWTService jwtService;
    private readonly IRepository<User> userRepo;

    public SecurityService(HashAlgorithm algorithm, Encoding encoding, IJWTService jwtService, IRepository<User> userRepo)
    {
        this.algorithm = algorithm;
        this.encoding = encoding;
        this.jwtService = jwtService;
        this.userRepo = userRepo;
    }

    public string HashPassword(string password, string salt)
    {
        var concatPassword = password + salt;
        var bytesPassword = encoding.GetBytes(concatPassword);

        var hashedPasswordBytes = algorithm.ComputeHash(bytesPassword);
        algorithm.Dispose();

        var hashedPassword = Convert.ToBase64String(hashedPasswordBytes);

        return hashedPassword;
    }

    public string GenerateSalt(int saltLength)
    {
        var length = (int)(saltLength * 0.75);
        byte[] randBytes = new byte[length];
        Random.Shared.NextBytes(randBytes);
        var salt = Convert.ToBase64String(randBytes);

        return salt;
    }

    public async Task<SecurityData> ValidateUserAsync(string jwt)
    {
        var validation = await jwtService.ValidateTokenAsync<JWTData>(jwt);
        var result = new SecurityData
        {
            User = null,
            Time = DateTime.Now
        };

        if (!validation.IsValid || validation.Data is null)
            return result;

        var date = DateTime.Parse(validation.Data.CreateAt);

        if (date.AddDays(-1) > DateTime.Now)
            return result;

        var user = await userRepo.Get(user => user.Id == validation.Data.UserId);

        result.User = user;
        result.Time = date;

        return result;
    }
}