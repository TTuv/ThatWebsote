using System.Net.Mail;
using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Testing
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var sent = await TestMigaduSmtpAsync("admin@dear-mary.am", "InfraBren2025-2026", "admin@dear-mary.am", "sry-goodbyemf@proton.me");
            //var sent = await TestMigaduSmtpAsync("gannouni.dhiaeddine@gmail.com", "qh:fN.Z_9SVT3aN", "admin@dear-mary.am", "sry-goodbyemf@proton.me");
        }
        public static async Task<bool> TestMigaduSmtpAsync(
        string username,
        string password,
        string fromEmail,
        string toEmail)
        {
            // 1. Create the email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Migadu Test Sender", fromEmail));
            message.To.Add(new MailboxAddress("Test Recipient", toEmail));
            message.Subject = "Migadu SMTP Programmatic Test";

            message.Body = new TextPart("plain")
            {
                Text = $"Hello! This is a test automated email sent via Migadu SMTP on {DateTime.UtcNow} UTC."
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

                Console.WriteLine("Success: Email sent programmatically through Migadu!");
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


    
