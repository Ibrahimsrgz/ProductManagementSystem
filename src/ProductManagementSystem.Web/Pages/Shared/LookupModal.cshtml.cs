using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProductManagementSystem.Web.Pages.Shared
{
    public abstract class LookupModalBase : ProductManagementSystemPageModel
    {
        public string CurrentId { get; set; }
        public string CurrentDisplayName { get; set; }

        public LookupModalBase()
        {
            CurrentId = string.Empty;
            CurrentDisplayName = string.Empty;
        }

        public virtual Task OnGetAsync(string currentId, string currentDisplayName)
        {
            CurrentId = currentId;
            CurrentDisplayName = currentDisplayName;

            return Task.CompletedTask;
        }
    }
}