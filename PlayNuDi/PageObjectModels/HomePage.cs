using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlayNuDi.PageObjectModels;

public class HomePage
{
    private readonly IConfiguration _config;
    private readonly IPage _page;

    public HomePage(IConfiguration config, IPage page)
    {
        _config = config;
        _page = page;
    }

    public async Task<string> GetDescription()
    {
        return await _page.InnerTextAsync(".podcast-info__description");
    }

    public async Task OpenPlatformPopup()
    {
        await _page.ClickAsync(".listen__item--more");
    }
}
