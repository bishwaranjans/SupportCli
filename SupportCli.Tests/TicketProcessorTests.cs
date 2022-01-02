#region Namespaces

using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SupportCli.Domain.Models;
using SupportCli.Infrastructure;
using System;
using System.Linq;

#endregion

namespace SupportCli.Tests;

public class TicketProcessorTests
{
    #region Fields

    private ILogger<TicketProcessor> _MockLogger;

    #endregion

    #region Set Up

    /// <summary>
    /// Setups this instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _MockLogger = Mock.Of<ILogger<TicketProcessor>>();
    }

    #endregion

    #region Test Methods

    [Test]
    public void CreateTicketSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        string input = "create Test";

        // Act
        var ticket = ticketProcessor.CreateTicket(input);

        // Assert
        Assert.AreEqual(1, ticket.Id);
    }

    [Test]
    public void CreateTicketUnSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        string input = "error";

        // Act and Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => ticketProcessor.CreateTicket(input));
    }

    [Test]
    public void ShowTicketSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        ticketProcessor.CreateTicket("create Test");
        int input = 1;

        // Act
        var ticket = ticketProcessor.GetTicketById(input);

        // Assert
        Assert.AreEqual(input, ticket.Id);
    }

    [Test]
    public void ShowTicketUnSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        ticketProcessor.CreateTicket("create Test");

        // Act 
        var ticket = ticketProcessor.GetTicketById(2);

        // Assert
        Assert.IsNull(ticket);
    }

    [Test]
    public void ListTicketsSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        ticketProcessor.CreateTicket("create Test");

        // Act
        var tickets = ticketProcessor.GetTickets();

        // Assert           
        Assert.AreEqual(1, tickets.Count);
        Assert.AreEqual("Test", tickets.FirstOrDefault().Title);
    }

    [Test]
    public void AddCommentSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        ticketProcessor.CreateTicket("create Test");
        string comment = "comment 1 TestComment";

        // Act
        var ticket = ticketProcessor.AddCommentToTicket(1, comment);

        // Assert
        Assert.AreEqual(comment, ticket.Comments.FirstOrDefault());
    }

    [Test]
    public void AddCommentUnSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        ticketProcessor.CreateTicket("create Test");
        string comment = "comment error TestComment";

        // Act 
        var ticket = ticketProcessor.AddCommentToTicket(2, comment);

        // Assert
        Assert.IsNull(ticket);
    }

    [Test]
    public void AssignUserSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        ticketProcessor.CreateTicket("create Test");
        string userName = "assign 1 TestUser";

        // Act
        var ticket = ticketProcessor.AssignUserToTicket(1, userName);

        // Assert
        Assert.AreEqual(userName, ticket.AssignedToUser);
        Assert.AreEqual(Ticket.State.InProgress, ticket.CurrentState);
    }

    [Test]
    public void AssignUserUnSuccessful()
    {
        var ticketProcessor = new TicketProcessor(_MockLogger);
        ticketProcessor.CreateTicket("create Test");
        string userName = "assign 1 TestUser";

        // Act 
        var ticket = ticketProcessor.AssignUserToTicket(2, userName);

        // Assert
        Assert.IsNull(ticket);
    }

    [Test]
    public void CloseTicketSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        ticketProcessor.CreateTicket("create Test");

        // Act
        var ticket = ticketProcessor.CloseTicket(1);

        // Assert
        Assert.AreEqual(1, ticket.Id);
        Assert.AreEqual(Ticket.State.Closed, ticket.CurrentState);
    }

    [Test]
    public void CloseTicketUnSuccessful()
    {
        // Prepare
        var ticketProcessor = new TicketProcessor(_MockLogger);
        ticketProcessor.CreateTicket("create Test");

        // Act 
        var ticket = ticketProcessor.CloseTicket(2);

        // Assert
        Assert.IsNull(ticket);
    }

    #endregion
}
