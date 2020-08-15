using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Netlog.Application.Models;
using Netlog.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Netlog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIndexPageService _indexPageService;

        public IndexModel(IIndexPageService indexPageService)
        {
            _indexPageService = indexPageService ?? throw new ArgumentNullException(nameof(indexPageService));
        }

        public IEnumerable<UserModel> UserList { get; set; } = new List<UserModel>();

        public async Task<IActionResult> OnGet()
        {
            UserList = await _indexPageService.GetUsers();

            //CategoryModel = await _indexPageService.GetCategoryWithUsers(1);
            //UserModel = await _indexPageService.GetUsers();
            return Page();
        }
    }
}
