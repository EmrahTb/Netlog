using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Netlog.Core.Entities;
using Netlog.Infrastructure.Data;
using Netlog.Web.Interfaces;
using Netlog.Application.Models;

namespace Netlog.Web.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly IUserPageService _UserPageService;

        public EditModel(IUserPageService UserPageService)
        {
            _UserPageService = UserPageService ?? throw new ArgumentNullException(nameof(UserPageService));
        }

        [BindProperty]
        public UserModel User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? UserId)
        {
            if (UserId == null)
            {
                return NotFound();
            }

            User = await _UserPageService.GetUserById(UserId.Value);
            if (User == null)
            {
                return NotFound();
            }
            
  
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try
            {
                await _UserPageService.UpdateUser(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            var User = _UserPageService.GetUserById(id);
            return User != null;            
        }
    }
}
