#region Namespaces

using System.Text;

#endregion

namespace SupportCli.Domain.Models;

/// <summary>
/// Ticket Model Entity
/// </summary>
public class Ticket
{
    #region Entity Data

    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public State CurrentState { get; set; }
    public string AssignedToUser { get; set; } = default!;
    public int CommentsCount { get; set; }
    public List<string> Comments { get; set; } = default!;

    public enum State
    {
        Open,
        InProgress,
        Closed
    }

    #endregion

    #region Entity Behaviour

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{nameof(Id)}={Id}");
        sb.AppendLine($"{nameof(Title)}={Title}");
        sb.AppendLine($"{nameof(CurrentState)}={CurrentState}");
        sb.AppendLine($"{nameof(AssignedToUser)}={AssignedToUser}");
        sb.AppendLine($"{nameof(CommentsCount)}={CommentsCount}");
        sb.AppendLine($"{nameof(Comments)}:");
        foreach (var comment in Comments)
        {
            sb.AppendLine($"> {comment}");
        }

        return sb.ToString();
    }

    #endregion
}

