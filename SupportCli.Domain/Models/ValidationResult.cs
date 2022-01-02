#region Namespaces

namespace SupportCli.Domain.Models;

#endregion

public class ValidationResult
{
    public bool IsSuccess { get; set; }
    public string Command { get; set; } = default!;
    public int TicketId { get; set; }
    public string AssignedUser { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public string Result { get; set; } = default!;
}

