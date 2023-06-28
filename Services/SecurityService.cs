using System.Security.Cryptography;
using System.Text;

namespace Coddit.Services;

public class SecurityService : ISecurityService
{
    private readonly HashAlgorithm _algorithm;
    private readonly Encoding _encoding;

    public SecurityService(Encoding encoding, HashAlgorithm algorithm)
    {
        _encoding = encoding;
        _algorithm = algorithm;
    }

    public string HashPassword(string password, string salt)
    {
        var concatPassword = password + salt;
        var bytesPassword = _encoding.GetBytes(concatPassword);

        var hashedPasswordBytes = _algorithm.ComputeHash(bytesPassword);
        _algorithm.Dispose();

        var hashedPassword = _encoding.GetString(hashedPasswordBytes);

        return hashedPassword;
    }

    public string GenerateSalt(int saltLength)
    {
        byte[] randBytes = new byte[saltLength];
        Random.Shared.NextBytes(randBytes);
        var salt = _encoding.GetString(randBytes);

        return salt;
    }
}