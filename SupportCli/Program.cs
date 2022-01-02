#region Namespaces

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SupportCli.Domain.Seedwork;
using SupportCli.Infrastructure;
using System;
using System.Text;

#endregion

var services = new ServiceCollection();

ConfigureServices(services);

var serviceProvider = services.BuildServiceProvider();
var ticketProcessor = serviceProvider.GetService<ITicketProcessor>();
var validator = serviceProvider.GetService<IValidator>();

Start();

#region Methods

void ConfigureServices(IServiceCollection services)
{
    services.AddLogging(configure => configure.AddConsole())
            .AddTransient<TicketProcessor>();

    services.AddSingleton<ITicketProcessor, TicketProcessor>();
    services.AddSingleton<IValidator, Validator>();
}

void Start()
{
    Display("\n\nSUPPORT CLI");

    while (true)
    {
        var sb = new StringBuilder();
        sb.AppendLine("\n\nAvailable commands:");
        sb.AppendLine("create %title% - create a new ticket");
        sb.AppendLine("show %ticket id% - show the ticket");
        sb.AppendLine("comment %ticket id% %comment% - add a comment to the ticket");
        sb.AppendLine("assign %ticket id% %user name% - assign the ticket");
        sb.AppendLine("close %ticket id% - close the ticket");
        sb.AppendLine("list- show all tickets");
        sb.AppendLine("q - exit");
        sb.AppendLine();
        Display(sb.ToString());

        var input = Console.ReadLine();
        if (input.Equals("q")) break;

        if (input.Equals("list"))
        {
            OperationList();
            continue;
        }

        if (input.StartsWith("create "))
        {
            OperationCreate(input);
            continue;
        }

        if (input.StartsWith("show "))
        {
            OperationShow(input);
            continue;
        }

        if (input.StartsWith("comment "))
        {
            OperationComment(input);
            continue;
        }

        if (input.StartsWith("assign "))
        {
            OperationAssign(input);
            continue;
        }

        if (input.StartsWith("close "))
        {
            OperationClose(input);
        }
    }
}

void OperationList()
{
    try
    {
        foreach (var ticket in ticketProcessor.GetTickets())
        {
            Display($"{ticket.Id} | {ticket.Title}");
        }
    }
    catch (Exception ex)
    {
        Display($"Something went wrong while trying to get the list of tickets {ex}");
    }
}

void OperationCreate(string input)
{
    try
    {
        var validationResult = validator.Validate(input);

        // If validation is success
        if (validationResult.IsSuccess)
        {
            var ticket = ticketProcessor.CreateTicket(input);
            Display($"Ticket with Id:{ticket.Id} has been created. ");
        }
        else
        {
            Display($"Validation failed. Please provide correct input as stated in helper command section. { validationResult.Result}");
        }
    }
    catch (Exception ex)
    {
        Display($"Something went wrong while trying to create ticket {ex}");
    }
}

void OperationShow(string input)
{
    try
    {
        var validationResult = validator.Validate(input);

        // If validation is success
        if (validationResult.IsSuccess)
        {
            var ticket = ticketProcessor.GetTicketById(validationResult.TicketId);
            Display(ticket is null ? $"Unable to find the ticket with id: {validationResult.TicketId}" : ticket.ToString());
        }
        else
        {
            Display($"Validation failed. Please provide correct input as stated in helper command section. { validationResult.Result}");
        }
    }
    catch (Exception ex)
    {
        Display($"Something went wrong while trying to show tickets {ex}");
    }
}

void OperationComment(string input)
{
    try
    {
        var validationResult = validator.Validate(input);

        // If validation is success
        if (validationResult.IsSuccess)
        {
            var ticket = ticketProcessor.AddCommentToTicket(validationResult.TicketId, validationResult.Comment);
            Display(ticket is null ? $"Unable to find the ticket with id: {validationResult.TicketId}" : $"new comment has been added into {validationResult.TicketId}");
        }
        else
        {
            Display($"Validation failed. Please provide correct input as stated in helper command section. { validationResult.Result}");
        }
    }
    catch (Exception ex)
    {
        Display($"Something went wrong while trying to add comment {ex}");
    }
}

void OperationAssign(string input)
{
    try
    {
        var validationResult = validator.Validate(input);

        // If validation is success
        if (validationResult.IsSuccess)
        {
            var ticket = ticketProcessor.AssignUserToTicket(validationResult.TicketId, validationResult.AssignedUser);
            Display(ticket is null ? $"Unable to find the ticket with id: {validationResult.TicketId}" : $"{validationResult.TicketId} has been assigned to {validationResult.AssignedUser}");
        }
        else
        {
            Display($"Validation failed. Please provide correct input as stated in helper command section. { validationResult.Result}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something went wrong while trying to add User {ex}");
    }
}

void OperationClose(string input)
{
    try
    {
        var validationResult = validator.Validate(input);

        // If validation is success
        if (validationResult.IsSuccess)
        {
            var ticket = ticketProcessor.CloseTicket(validationResult.TicketId);
            Display(ticket is null ? $"Unable to find the ticket with id: {validationResult.TicketId}" : $"{validationResult.TicketId} has been closed ");
        }
        else
        {
            Display($"Validation failed. Please provide correct input as stated in helper command section. { validationResult.Result}");
        }
    }
    catch (Exception ex)
    {
        Display($"Something went wrong while trying to close Ticket{ex}");
    }
}

void Display(string message)
{
    Console.WriteLine(message);
}

#endregion