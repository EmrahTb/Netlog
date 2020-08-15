using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netlog.Web.Interfaces;
using Netlog.Application.Models;
using Netlog.Core.Entities;

namespace Netlog.Web.Pages.Maintenance
{
    public class DetailsModel : PageModel
    {
        private readonly IMaintenancePageService _MaintenancePageService;

        public DetailsModel(IMaintenancePageService MaintenancePageService)
        {
            _MaintenancePageService = MaintenancePageService ?? throw new ArgumentNullException(nameof(MaintenancePageService));
        }       

        public MaintenanceReport Maintenance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? MaintenanceId)
        {
            if (MaintenanceId == null)
            {
                return NotFound();
            }

            Maintenance = await _MaintenancePageService.GetMaintenanceDetail(MaintenanceId.Value);
            if (Maintenance == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
