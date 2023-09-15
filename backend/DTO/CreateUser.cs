namespace Coddit.DTO;

public class CreateUser
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
}