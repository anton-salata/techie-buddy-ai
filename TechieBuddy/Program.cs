using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using TechieBuddy.Core.Services;

namespace TechieBuddy
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter Software:");
            string software = Console.ReadLine();

            Console.WriteLine("Enter Question:");
            string question = Console.ReadLine();

            Console.WriteLine("Processing...");
            var techieBuddyAIService = new TechieBuddyAIService();
            var response = await techieBuddyAIService.AnswerQuestionAsync(software, question);

            Console.WriteLine(response);
        }

    }
}