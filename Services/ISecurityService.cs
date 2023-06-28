namespace Coddit.Services;

public interface ISecurityService
{
    string GenerateSalt(int saltLength);
    string HashPassword(string password, string salt);
}