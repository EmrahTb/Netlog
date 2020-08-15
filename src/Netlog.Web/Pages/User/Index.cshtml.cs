using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netlog.Web.Interfaces;
using Netlog.Application.Models;

namespace Netlog.Web.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IUserPageService _UserPageService;

        public IndexModel(IUserPageService UserPageService)
        {
            _UserPageService = UserPageService ?? throw new ArgumentNullException(nameof(UserPageService));
        }

        public IEnumerable<UserModel> UserList { get; set; } = new List<UserModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserList = await _UserPageService.GetUsers(SearchTerm);
            return Page();
        }
    }
}
