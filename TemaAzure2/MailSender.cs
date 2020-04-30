using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.SignalR;

namespace TemaAzure2
{
	public class MailSender:Hub
	{
        public async Task sendMailTo(string email)
        {
            await Execute(email);
            await Clients.Client(Context.ConnectionId).SendAsync("sendMailTo");

        }


        public async Task Execute(string email)
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
