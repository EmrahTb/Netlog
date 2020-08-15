using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Netlog.Core.Entities;
using Netlog.Infrastructure.Data;
using Netlog.Web.Interfaces;
using Netlog.Application.Models;

namespace Netlog.Web.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly IUserPageService _UserPageService;

        public CreateModel(IUserPageService UserPageService)
        {
            _UserPageService = UserPageService ?? throw new ArgumentNullException(nameof(UserPageService));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _UserPageService.GetUsers("");
            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            return Page();
        }

        [BindProperty]
        public UserModel User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User = await _UserPageService.CreateUser(User);
            return RedirectToPage("./Index");
        }
    }
}