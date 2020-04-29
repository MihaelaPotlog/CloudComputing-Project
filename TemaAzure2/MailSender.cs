using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.SignalR;

namespace TemaAzure2
{
	public class MailSender:Hub
	{
        static void sendMailTo(string email)
        {
            Execute(email).Wait();

        }


        static async Task Execute(string email)
        {
            Console.WriteLine("sending email...");

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("foodtalk@example.com", "FoodTalk Team"),
                Subject = "Invitation in FoodTalk app!",
                PlainTextContent = "",
                HtmlContent = "<strong>Hello! You have an invitation from UserName on FoodTalk. </strong>"

            };

            msg.AddTo(new EmailAddress(email));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);
        }
    }
}
