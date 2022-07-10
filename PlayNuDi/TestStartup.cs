using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright.NUnit;
using PlayNuDi.PageObjectModels;
using System;

namespace PlayNuDi;

public class TestStartUp : PageTest
{
    private IServiceProvider _serviceProvider;
    private IConfiguration _configuration;
    private TokenCredential _tokenCredential;

    public TestStartUp()
    {
        _configuration = BuildConfiguration();
        _serviceProvider = BuildServices();
        _tokenCredential = new DefaultAzureCredential();
    }

    private IConfiguration BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .AddJsonFile("appsettings.dev.json", optional: true);


        return builder.Build();
    }


    private IServiceProvider BuildServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton(_configuration);

        services.AddSingleton<SecretClient>(s =>
           new(new Uri(_configuration["keyvaultUrl"]), _tokenCredential));

        services.AddTransient(page => Page);
        services.AddTransient<HomePage>();
        services.AddTransient<PlatformPopUp>();

        return services.BuildServiceProvider();
    }

    public IConfiguration Configuration => _configuration;
    public T GetService<T>() => _serviceProvider.GetService<T>() ?? throw new NullReferenceException("Do not forget to register the service.");

}