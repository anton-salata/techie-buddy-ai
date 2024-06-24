using PuppeteerSharp;

namespace TechieBuddy.Core.Services
{
    public class TechieBuddyAIService : ITechieBuddyAIService
    {
        public async Task<string> AnswerQuestionAsync(string software, string question)
        {
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Browser = SupportedBrowser.Chrome,
                ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                DefaultViewport = new ViewPortOptions
                {
                    Width = 1000,
                    Height = 3000
                }

            });

            using var page = await browser.NewPageAsync();
            await page.GoToAsync("https://zzzcode.ai/answer-question");

            await page.TypeAsync("#uiP1", software);
            await page.TypeAsync("#uiP2", question);

            await page.ClickAsync("#uiActionButton");

            await Task.Delay(5000);

            // Extract the response
            var pageContent = await page.GetContentAsync();

            // Then extract the content
            var textareaElement = await page.QuerySelectorAsync("#uiOutputContent");
            var textareaContent = string.Empty;

            if (textareaElement == null)
            {
                var questionId = await page.EvaluateExpressionAsync<string>("document.getElementById('uiMessageID').value");
                await page.GoToAsync($"https://zzzcode.ai/answer-question?id={questionId}");

                await Task.Delay(3000);

                pageContent = await page.GetContentAsync();
                textareaContent = await page.EvaluateExpressionAsync<string>("document.getElementById('uiOutputHtml').innerHTML");
            }
            else
            {
                textareaContent = await textareaElement.EvaluateFunctionAsync<string>("el => el.value");
            }

            await browser.CloseAsync();

            return textareaContent;
        }
    }
}
