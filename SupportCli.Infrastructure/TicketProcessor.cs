#region Namespaces

using Microsoft.Extensions.Logging;
using SupportCli.Domain.Models;
using SupportCli.Domain.Seedwork;

#endregion

namespace SupportCli.Infrastructure;

/// <summary>
/// Ticket Processor Implementation
/// </summary>
public class TicketProcessor : ITicketProcessor
{
    #region Fields

    private readonly Dictionary<int, Ticket> _tickets;
    private int _counter;

    private readonly ILogger _logger;

    #endregion

    #region Ctors

    /// <summary>Initializes a new instance of the <see cref="TicketProcessor" /> class.</summary>
    /// <param name="logger">The logger.</param>
    public TicketProcessor(ILogger<TicketProcessor> logger)
    {
        _logger = logger;
        _tickets = new Dictionary<int, Ticket>();
        _counter = 0;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets all tickets
    /// </summary>
    /// <returns>Returns tickets.</returns>
    public List<Ticket> GetTickets()
    {
        try
        {
            return _tickets.Values.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    /// <summary>
    /// Creates a new Ticket
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>Returns true if the creation is successful.</returns>
    public Ticket CreateTicket(string input)
    {
        try
        {
            _counter++;
            _tickets[_counter] = new Ticket
            {
                Id = _counter,
                Title = input.Substring(7, input.Length - 7),
                CurrentState = Ticket.State.Open,
                Comments = new List<string>()
            };
            return _tickets[_counter];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    /// <summary>
    /// Gets the ticket by identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns the ticket details.</returns>
    public Ticket? GetTicketById(int id)
    {
        try
        {
            if (!_tickets.ContainsKey(id))
            {
                return null;
            }

            return _tickets[id];
        }

        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    /// <summary>
    /// Adds the comment to ticket.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="comment">The comment.</param>
    /// <returns>Returns the ticket information.</returns>
    public Ticket? AddCommentToTicket(int id, string comment)
    {
        try
        {
            if (!_tickets.ContainsKey(id))
            {
                return null;
            }

            var ticket = _tickets[id];

            ticket.Comments.Add(comment);
            ticket.CommentsCount++;

            return ticket;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }

    }

    /// <summary>
    /// Assigns the user to ticket.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="userName">Name of the user.</param>
    /// <returns>Returns the ticket information.</returns>
    public Ticket? AssignUserToTicket(int id, string userName)
    {
        try
        {
            if (!_tickets.ContainsKey(id))
            {
                return null;
            }

            var ticket = _tickets[id];

            ticket.Comments.Add($"Assigned {userName} at {DateTime.UtcNow}");
            ticket.CommentsCount++;
            ticket.AssignedToUser = userName;
            ticket.CurrentState = Ticket.State.InProgress;

            return ticket;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    /// <summary>
    /// Closes the ticket.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns ticket details.</returns>
    public Ticket? CloseTicket(int id)
    {
        try
        {
            if (!_tickets.ContainsKey(id))
            {
                return null;
            }

            var ticket = _tickets[id];

            ticket.CurrentState = Ticket.State.Closed;
            ticket.Comments.Add("closed " + DateTime.UtcNow);
            ticket.CommentsCount++;

            return ticket;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    #endregion
}

