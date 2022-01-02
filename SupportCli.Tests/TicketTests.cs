using NUnit.Framework;
using System;
using System.IO;

namespace SupportCli.Tests
{
    public class TicketTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateTicketSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            string input = "create Test";
            
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            
            ticketProcessor.CreateTicket(input);
            
            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("1 has been created \r\n", output);
        }

        [Test]
        public void CreateTicketUnSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            string input = "error";

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            ticketProcessor.CreateTicket(input);

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("Something went wrong", output.Substring(0, 20));
        }

        [Test]
        public void ShowTicketSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            ticketProcessor.CreateTicket("create Test");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string input = "show 1";
            ticketProcessor.ShowTicket(input);

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("Id=1\r\nTitle=Test\r\nCurrentState=Open\r\nAssignedToUser=\r\nCommentsCount=0\r\nComments:\r\n", output);
        }

        [Test]
        public void ShowTicketUnSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            ticketProcessor.CreateTicket("create Test");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string input = "show error";
            ticketProcessor.ShowTicket(input);

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("Something went wrong", output.Substring(0, 20));
            
        }

        [Test]
        public void ListTicketsSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            ticketProcessor.CreateTicket("create Test");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string input = "list";
            ticketProcessor.ListTickets();

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("1 | Test\r\n", output);
        }

        [Test]
        public void AddCommentSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            ticketProcessor.CreateTicket("create Test");
            
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            
            string input = "comment 1 TestComment";
            ticketProcessor.AddComment(input);

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("new comment has been added into 1\r\n", output);
        }

        [Test]
        public void AddCommentUnSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            ticketProcessor.CreateTicket("create Test");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string input = "comment error TestComment";
            ticketProcessor.AddComment(input);

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("Something went wrong", output.Substring(0,20));
        }

        [Test]
        public void AssignUserSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            ticketProcessor.CreateTicket("create Test");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string input = "assign 1 TestUser";
            ticketProcessor.AssignUser(input);

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("1 has been assigned to TestUser\r\n", output);
        }

        [Test]
        public void AssignUserUnSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            ticketProcessor.CreateTicket("create Test");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string input = "assign error TestUser";
            ticketProcessor.AssignUser(input);

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("Something went wrong", output.Substring(0, 20));
        }

        [Test]
        public void CloseTicketSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            ticketProcessor.CreateTicket("create Test");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string input = "close 1";
            ticketProcessor.CloseTicket(input);

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("1 has been closed \r\n", output);
        }

        [Test]
        public void CloseTicketUnSuccessful()
        {
            var ticketProcessor = new TicketProcessor();
            ticketProcessor.CreateTicket("create Test");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string input = "close error";
            ticketProcessor.CloseTicket(input);

            //assert
            var output = stringWriter.ToString();
            Assert.AreEqual("Something went wrong", output.Substring(0, 20));
        }
    }
}