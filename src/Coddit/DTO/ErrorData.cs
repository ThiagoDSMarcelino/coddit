#pragma warning disable CS8618

namespace Coddit.DTO;

public class ErrorData
{
    public string[] Messages { get; set; }

    public string Reason { get; set; }
}