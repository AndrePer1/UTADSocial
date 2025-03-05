using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PL1_G1_1.Areas.Identity.Pages.Account
{
    public class FogotPasswordViewModel : PageModel
    {
        public void OnGet()
        {
        }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
    }
}
