using Azure.Security.KeyVault.Secrets;
using NUnit.Framework;
using PlayNuDi.PageObjectModels;
using System.Threading.Tasks;

namespace PlayNuDi;

[Parallelizable(ParallelScope.Self)]
public class Tests : TestStartUp
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CheckHomePage()
    {
        await Page.GotoAsync(Configuration["BaseUrl"]);
        var homePage = GetService<HomePage>();
        var description = await homePage.GetDescription();

        Assert.IsNotNull(description);
        Assert.IsTrue(description.StartsWith("Welcome to Betatalks the podcast"));

       
    }

    [Test]
    public async Task CheckPlatforms()
    {
        await Page.GotoAsync(Configuration["BaseUrl"]);
        var homePage = GetService<HomePage>();

        await homePage.OpenPlatformPopup();
        var platFormPopUp = GetService<PlatformPopUp>();

        Assert.IsTrue(await platFormPopUp.HasSpotify());
    }

    [Test]
    public async Task Login()
    {
        await Page.GotoAsync(Configuration["BaseUrl"]);
        
        var secretClient = GetService<SecretClient>();
        var secretVault = await secretClient.GetSecretAsync("Somesecretname");
    }
}