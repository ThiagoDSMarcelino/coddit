using System.Security.Cryptography;
using System.Text;

namespace Coddit.Services;

public class SecurityService : ISecurityService
{
    private readonly HashAlgorithm _algorithm;
    private readonly Encoding _encoding;

    public SecurityService(Encoding encoding, HashAlgorithm algorithm)
    {
        this._encoding = encoding;
        this._algorithm = algorithm;
    }

    public string HashPassword(string password, string salt)
    {
        var concatPassword = password + salt;
        var bytesPassword = _encoding.GetBytes(concatPassword);

        var hashedPasswordBytes = _algorithm.ComputeHash(bytesPassword);
        _algorithm.Dispose();

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