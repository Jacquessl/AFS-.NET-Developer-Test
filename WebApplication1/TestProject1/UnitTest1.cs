using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Repositories;

namespace TestProject1;

public class UnitTest1
{

    [Fact]
    public async Task TooLongText()
    {
        var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        var appsettingsPath = Path.Combine(projectDirectory, "WebApplication1", "appsettings.json");
        var translatorController = new TranslatorController(new TranslatorRepository(new ConfigurationBuilder().AddJsonFile(appsettingsPath).Build()));
        var result = await translatorController.Translate("dasjodiaaojdaolijdaokpjldalkdadkladasjodiaaojdaolijdao" +
                                       "kpjldalkdadkladasjodiaaojdaolijdaokpjldalkdadkladasjodiaaojdaolijdaokpjldalk" +
                                       "dadkladasjodiaaojdaolijdaokpjldalkdadkladasjodiaaojdaolijdaokpjldalkdadkladasjo" +
                                       "diaaojdaolijdaokpjldalkdadkladasjodiaaojdaolijdaokpjldalkdadkla");
        var actionResult = result as OkObjectResult;
        actionResult.Should().NotBeNull();
    }
    
    [Fact]
    public async Task EmptyText()
    {
        var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        var appsettingsPath = Path.Combine(projectDirectory, "WebApplication1", "appsettings.json");
        var translatorController = new TranslatorController(new TranslatorRepository(new ConfigurationBuilder().AddJsonFile(appsettingsPath).Build()));
        var result = await translatorController.Translate("");
        var actionResult = result as OkObjectResult;
        actionResult.Should().NotBeNull();
    }

    [Fact]
    public async Task NormalText()
    {
        var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        var appsettingsPath = Path.Combine(projectDirectory, "WebApplication1", "appsettings.json");
        var translatorController = new TranslatorController(new TranslatorRepository(new ConfigurationBuilder().AddJsonFile(appsettingsPath).Build()));
        var result = await translatorController.Translate("Normal Text");
        var actionResult = result as OkObjectResult;
        actionResult.Should().NotBeNull();
    }

    [Fact]
    public async Task RepoGetNormalText()
    {
        var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        var appsettingsPath = Path.Combine(projectDirectory, "WebApplication1", "appsettings.json");
        var translatorRepository = new TranslatorRepository(new ConfigurationBuilder().AddJsonFile(appsettingsPath).Build());
        var result = await translatorRepository.GetTranslation("Normal Text");
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task DBReturnsNormalText()
    {
        var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        var appsettingsPath = Path.Combine(projectDirectory, "WebApplication1", "appsettings.json");
        var translatorRepository = new TranslatorRepository(new ConfigurationBuilder().AddJsonFile(appsettingsPath).Build());
        var result = await translatorRepository.GetTranslationIfExists("Normal Text");
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task DBAddsResult()
    {
        var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        var appsettingsPath = Path.Combine(projectDirectory, "WebApplication1", "appsettings.json");
        var translatorRepository = new TranslatorRepository(new ConfigurationBuilder().AddJsonFile(appsettingsPath).Build());
        await translatorRepository.AddTranslation("test123", "test123");
        var result = await translatorRepository.GetTranslationIfExists("test123");
        result.Should().NotBeNull();
        result.Should().Be("test123");
    }
}