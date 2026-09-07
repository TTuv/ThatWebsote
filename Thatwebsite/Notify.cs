using MailKit.Security;
using MimeKit;
namespace Thatwebsite
{
    public class Notify
    {
        

        public static async Task Loggin()
        {
            await SendMessage("admin@dear-mary.am", "InfraBren2025-2026", "admin@dear-mary.am", "sry-goodbyemf@proton.me");
        }
        private static async Task<bool> SendMessage(
          string username,
          string password,
          string fromEmail,
          string toEmail)
        {
            // 1. Create the email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Postmaster", fromEmail));
            message.To.Add(new MailboxAddress("Receiver", toEmail));
            message.Subject = "Login information";

            message.Body = new TextPart("plain")
            {
                Text = $"User Logged in Successfully on{DateTime.UtcNow} UTC."
            };

            // 2. Connect and send using MailKit
            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                // Connect to Migadu using recommended SSL/TLS Port 465
                await client.ConnectAsync("smtp.migadu.com", 465, SecureSocketOptions.SslOnConnect);

                // Authenticate with your full mailbox address and password
                await client.AuthenticateAsync(username, password);

                // Send the message
                await client.SendAsync(message);
                await Task.Delay(500);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"SMTP Test Failed: {ex.Message}");
                return false;
            }   
            finally
            {
                // Cleanly disconnect from the server
                await client.DisconnectAsync(true);
            }
        }
    }
}
