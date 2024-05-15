using System.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class TranslatorRepository : ITranslatorRepository
{
    private readonly IConfiguration _configuration;
    private ITranslatorRepository _translatorRepositoryImplementation;

    public TranslatorRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    async public Task<string> GetTranslation(string text)
    {
        string url = "https://api.funtranslations.com/translate/leetspeak.json";
        using (HttpClient httpClient = new HttpClient())
        {

            var requestData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", text)
            });

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, requestData);
                
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic jsonObject = JsonConvert.DeserializeObject(json);
                    string translatedText = jsonObject.contents.translated;
                    
                   
                   return translatedText;
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception occured: {ex.Message}";
            }
        }    
    }

    async public Task<string> GetTranslationIfExists(string text)
    {
        var query = "SELECT Translation FROM Translation WHERE Text = @text";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@text", text);
        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        var translation = reader.GetOrdinal("Translation");
        await reader.ReadAsync();
        try
        {
            return reader.GetString(translation);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    async public Task AddTranslation(string text, string translation)
    {

        var query = "INSERT INTO Translation(Text, Translation) VALUES (@text, @translation)";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@text", text);
        command.Parameters.AddWithValue("@translation", translation);
        await connection.OpenAsync();
        await command.ExecuteScalarAsync();
    }
}