using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportCli
{
    public class TicketProcessor
    {
        private readonly Dictionary<int, Ticket> _tickets = new Dictionary<int, Ticket>();
        private int _counter = 0;

        public void Start()
        {
            Console.WriteLine("\n\nSUPPORT CLI");
            while (true)
            {
                Console.WriteLine("\n\nAvailable commands:");
                Console.WriteLine("create %title% - create a new ticket");
                Console.WriteLine("show %ticket id% - show the ticket");
                Console.WriteLine("comment %ticket id% %comment% - add a comment to the ticket");
                Console.WriteLine("assign %ticket id% %user name% - assign the ticket");
                Console.WriteLine("close %ticket id% - close the ticket");
                Console.WriteLine("list- show all tickets");
                Console.WriteLine("q - exit");
                Console.WriteLine();

                var input = Console.ReadLine();
                if (input.Equals("q")) break;

                if (input.Equals("list"))
                {
                    ListTickets();
                    continue;
                }

                if (input.StartsWith("create "))
                {
                    CreateTicket(input);
                    continue;
                }

                if (input.StartsWith("show "))
                {
                    ShowTicket(input);
                    continue;
                }

                if (input.StartsWith("comment "))
                {
                    AddComment(input);
                    continue;
                }

                if (input.StartsWith("assign "))
                {
                    AssignUser(input);
                    continue;
                }

                if (input.StartsWith("close "))
                {
                    CloseTicket(input);
                }
            }
        }

        /// <summary>
        /// Lists all availabe tickets
        /// </summary>
        internal void ListTickets()
        {
            try
            {
                foreach (var ticket in _tickets)
                {
                    Console.WriteLine($"{ticket.Value.Id} | {ticket.Value.Title}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong while trying to List tickets {ex}");
            }
        }

        /// <summary>
        /// Creates a new Ticket 
        /// </summary>
        /// <param name="input"></param>
        internal void CreateTicket(string input)
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
                Console.WriteLine($"{_counter} has been created ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong while trying to create ticket {ex}");
            }
        }

        /// <summary>
        /// Shows the details of a ticket
        /// </summary>
        /// <param name="input"></param>
        internal void ShowTicket(string input)
        {
            try
            {
                var id = int.Parse(input.Split(' ')[1]);
                _tickets[id].Show();
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong while trying to show tickets {ex}");
            }
        }

        /// <summary>
        /// Adds a comment to a ticket
        /// </summary>
        /// <param name="input"></param>
        internal void AddComment(string input)
        {
            try
            {
                var id = int.Parse(input.Split(' ')[1]);
                var commentPrefix = 8 + input.Split(' ')[1].Length + 1;
                var comment = input.Substring(commentPrefix, input.Length - commentPrefix);
                var ticket = _tickets[id];
                ticket.Comments.Add(comment);
                ticket.CommentsCount++;
                Console.WriteLine($"new comment has been added into {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong while trying to add comment {ex}");
            }
            
        }

        /// <summary>
        /// Assigns a user to a ticket 
        /// </summary>
        /// <param name="input"></param>
        internal void AssignUser(string input)
        {
            try
            {
                var id = int.Parse(input.Split(' ')[1]);
                var usernamePrefix = 7 + input.Split(' ')[1].Length + 1;
                var username = input.Substring(usernamePrefix, input.Length - usernamePrefix);
                var ticket = _tickets[id];
                ticket.Comments.Add("assigned " + username + " " + DateTime.UtcNow);
                ticket.CommentsCount++;
                ticket.AssignedToUser = username;
                ticket.CurrentState = Ticket.State.InProgress;
                Console.WriteLine($"{id} has been assigned to {username}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong while trying to add User {ex}");
            }
        }

        /// <summary>
        /// Sets the status of a ticket to closed
        /// </summary>
        /// <param name="input"></param>
        internal void CloseTicket(string input)
        {
            try
            {
                var id = int.Parse(input.Split(' ')[1]);
                var ticket = _tickets[id];
                ticket.CurrentState = Ticket.State.Closed;
                ticket.Comments.Add("closed " + DateTime.UtcNow);
                ticket.CommentsCount++;
                Console.WriteLine($"{id} has been closed ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong while trying to close Ticket{ex}");
            }
        }

    }
}
