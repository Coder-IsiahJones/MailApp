using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace MailApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create a new mime message object which we are going to use to fill the message data.
            MimeMessage message = new MimeMessage();

            // Add the sender info that will appear in the email message.
            message.From.Add(new MailboxAddress("Coder Isiah", "coder.isiah.jones@gmail.com"));

            // Add the receiver email address.
            message.To.Add(MailboxAddress.Parse("coder.isiah.jones@gmail.com"));

            // Add the message subject.
            message.Subject = "Test Email";

            // Add the message body as plain text string passed to the TextPart.
            // Indicates that it's plain text and not HTML for example.
            message.Body = new TextPart("plain")
            {
                Text = @"Yes,
                This is a test email using MailKit."
            };

            // Ask the user to enter the email.
            Console.Write("Email: ");
            string emailAddress = Console.ReadLine();

            // Ask the user to enter the password
            Console.Write("Password: ");

            // For security reasons we need to mask the password, to do that we will turn the console's background to a certain color.
            // Store original values of the console's background and foreground color.
            ConsoleColor originalBGColor = Console.BackgroundColor;
            ConsoleColor originalFGColor = Console.ForegroundColor;

            // Set the console's background and foreground colors to red for example.
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;

            // Read the password.
            string password = Console.ReadLine();

            // Set the console's background and foreground colors to the original values we store earlier.
            Console.BackgroundColor = originalBGColor;
            Console.ForegroundColor = originalFGColor;

            // Create a new SMTP client.
            SmtpClient client = new SmtpClient();

            try
            {
                // Connect to the Gmail SMTP server using port 465 with SSL enabled.
                client.Connect("smtp.gmail.com", 465, true);

                // Note: only needed if the SMTP server requires authentication, Gmail does for example.
                client.Authenticate(emailAddress, password);

                // Send message.
                client.Send(message);

                // Display a message if no exception was thrown.
                Console.WriteLine("Email Sent!");
            }
            catch (Exception ex)
            {
                // In case of an error display the message
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // At any case always disconnect from the client.
                client.Disconnect(true);

                // Dispose of the client object.
                client.Dispose();
            }

            Console.ReadLine();
        }
    }
}