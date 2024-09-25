using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace ProductManagementSystem.Web.Pages;

public class IndexModel : ProductManagementSystemPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
