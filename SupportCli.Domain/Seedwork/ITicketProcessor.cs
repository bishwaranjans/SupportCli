#region Namespaces

using SupportCli.Domain.Models;

#endregion

namespace SupportCli.Domain.Seedwork;

/// <summary>
/// Contract for Ticket processor
/// </summary>
public interface ITicketProcessor
{
    /// <summary>
    /// Gets all tickets
    /// </summary>
    /// <returns>Returns tickets.</returns>
    List<Ticket> GetTickets();

    /// <summary>
    /// Creates a new Ticket
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>Returns true if the creation is successful.</returns>
    Ticket CreateTicket(string input);

    /// <summary>
    /// Gets the ticket by identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns the ticket details.</returns>
    Ticket? GetTicketById(int id);

    /// <summary>
    /// Adds the comment to ticket.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="comment">The comment.</param>
    /// <returns>Returns the ticket information.</returns>
    Ticket? AddCommentToTicket(int id, string comment);

    /// <summary>
    /// Assigns the user to ticket.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="userName">Name of the user.</param>
    /// <returns>Returns the ticket information.</returns>
    Ticket? AssignUserToTicket(int id, string userName);

    /// <summary>
    /// Closes the ticket.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns ticket details.</returns>
    Ticket? CloseTicket(int id);
}

