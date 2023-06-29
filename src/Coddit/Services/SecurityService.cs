using System.Security.Cryptography;
using System.Text;

namespace Coddit.Services;

public class SecurityService : ISecurityService
{
    private readonly HashAlgorithm algorithm;
    private readonly Encoding encoding;

    public SecurityService(Encoding encoding, HashAlgorithm algorithm)
    {
        this.encoding = encoding;
        this.algorithm = algorithm;
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
}