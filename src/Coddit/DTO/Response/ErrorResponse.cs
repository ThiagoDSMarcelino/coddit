namespace Coddit.DTO.Response;

public class ErrorResponse
{
    public string[] Messages { get; set; }

    public string Reason { get; set; }
}