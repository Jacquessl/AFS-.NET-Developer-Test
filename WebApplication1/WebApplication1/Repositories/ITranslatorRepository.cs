namespace WebApplication1.Repositories;

public interface ITranslatorRepository
{
    Task<string> GetTranslation(string text);
    Task<string> GetTranslationIfExists(string text);
    Task AddTranslation(string text, string translation);
}