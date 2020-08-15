using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netlog.Web.Interfaces;
using Netlog.Application.Models;
using Netlog.Core.Entities;

namespace Netlog.Web.Pages.Maintenance
{
    public class IndexModel : PageModel
    {
        private readonly IMaintenancePageService _MaintenancePageService;

        public IndexModel(IMaintenancePageService MaintenancePageService)
        {
            _MaintenancePageService = MaintenancePageService ?? throw new ArgumentNullException(nameof(MaintenancePageService));
        }

        public IEnumerable<MaintenanceReport> MaintenanceList { get; set; } = new List<MaintenanceReport>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            MaintenanceList = await _MaintenancePageService.GetMaintenances();
            return Page();
        }
    }
}
