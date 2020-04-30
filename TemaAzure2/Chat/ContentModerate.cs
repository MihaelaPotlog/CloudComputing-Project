using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;


namespace TemaAzure2.Chat
{
	public class ContentModerate
	{
        private static readonly string SubscriptionKey = "99fa7073b5ab49e3b69ce6e798d77257";
        private static readonly string Endpoint = "https://foodtalkcontentmoderator.cognitiveservices.azure.com/";

        public static bool ExistsBadWords(string message)
        {
            ContentModeratorClient clientText = Authenticate(SubscriptionKey, Endpoint);
            return ModerateText(clientText, message);

        }


        public static ContentModeratorClient Authenticate(string key, string endpoint)
        {
            ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(key));
            client.Endpoint = endpoint;
            return client;
        }


        public static bool ModerateText(ContentModeratorClient client, string text)
        {
            Console.WriteLine("TEXT MODERATION");


            // Remove carriage returns
            text = text.Replace(Environment.NewLine, " ");
            // Convert string to a byte[], then into a stream (for parameter in ScreenText()).
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream(textBytes);


            using (client)
            {
                // Moderate the text
                var screenResult = client.TextModeration.ScreenText("text/plain", stream, "eng", true, true, null, true);
                //var badWords = JsonConvert.SerializeObject(screenResult, Formatting.Indented);
                if (screenResult.Terms == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }


        }
    }
}
