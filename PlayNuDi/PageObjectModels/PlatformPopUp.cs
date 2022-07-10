using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlayNuDi.PageObjectModels;

public class PlatformPopUp
{
    private readonly IPage _page;

    public PlatformPopUp(IPage page)
    {
        _page = page;
    }

    public async Task<bool> HasSpotify()
    {
        return await _page.InnerTextAsync(".listen-modal__item--spotify") == "Spotify";
    }

}
