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
    
    [HttpGet("{text}")]
    public async Task<IActionResult> Translate(string text)
    {
        string result;
        if((result = await _translatorRepository.GetTranslationIfExists(text)) is not null)
        {
            return Ok(result);
        }
        var response = await _translatorRepository.GetTranslation(text);
        await _translatorRepository.AddTranslation(text, response);
        return Ok(response);
        
    }

}