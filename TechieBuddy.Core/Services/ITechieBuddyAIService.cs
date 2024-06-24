namespace TechieBuddy.Core.Services
{
    public interface ITechieBuddyAIService
    {
        Task<string> AnswerQuestionAsync(string software, string question);
    }
}