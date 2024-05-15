using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Repositories;

[Route("[controller]")]
[ApiController]
public class TranslatorController : ControllerBase
{
    private readonly ITranslatorRepository _translatorRepository;

    public TranslatorController(ITranslatorRepository translatorRepository)
    {
        _translatorRepository = translatorRepository;
    }
    
    [HttpGet("{text?}")]
    public async Task<IActionResult> Translate(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            text = "There is no text to translate";
        }
        if (text.Length > 200)
        {
            text = "Text cannot exceed 200 characters";
        }
        string result;
        if((result = await _translatorRepository.GetTranslationIfExists(text)) is not null)
        {
            return Ok(result);
        }
        var response = await _translatorRepository.GetTranslation(text);
        if (!response.Contains("Error"))
        {
            await _translatorRepository.AddTranslation(text, response);
        }

        return Ok(response);
        
    }

}