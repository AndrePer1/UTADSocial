// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using PL1_G1_1.Data;

namespace PL1_G1_1.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _he;

        public EmailModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            ApplicationDbContext context,
            IHostEnvironment he)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
            _he = he;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [EmailAddress]
        [Display(Name = "Email atual")]
        public string Email { get; set; }


        [Display(Name = "Morada atual")]
        public string Morada { get; set; }

        
        [Display(Name = "Data de nascimento atual")]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }


        public string Foto { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>      
            [Display(Name = "Data de nascimento atualizada")]
            [DataType(DataType.Date)]
            public DateTime DataNova { get; set; }

            [Display(Name = "Nova foto de perfil")]
            [RegularExpression(@"^.+\.(jpg|png)$", ErrorMessage = "Só ficheiros .jpg ou .png!")]
            public string FotoNova { get; set; }

            [EmailAddress]
            [Display(Name = "Novo email")]
            public string NewEmail { get; set; }


            [Display(Name = "Nova morada")]
            public string NewMorada { get; set; }

        }

        private async Task LoadAsync(IdentityUser user)
        {
            string utilizador = User.Identity.Name;

            string emailAntigo = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.Email)
                                        .FirstOrDefault();
            Email = emailAntigo;


            string MoradaAntiga = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.Morada)
                                        .FirstOrDefault();
            Morada = MoradaAntiga;

            DateTime DataAntiga = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.dtNasc)
                                        .FirstOrDefault();
            Nascimento = DataAntiga;


            string FotoAntiga = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.FotoPerf)
                                        .FirstOrDefault();
            Foto = FotoAntiga;

            Input = new InputModel
            {
                NewEmail = emailAntigo,
                NewMorada = MoradaAntiga,
                DataNova = DataAntiga,
                FotoNova = FotoAntiga,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }


            string utilizador = User.Identity.Name;

            string emailAntigo = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.Email)
                                        .FirstOrDefault();

            string Novoemail = Input.NewEmail;

            var emailOLD = _context.Autenticados.FirstOrDefault(x => x.Email == emailAntigo);

            if (emailOLD != null)
            {
                emailOLD.Email = Novoemail;

                _context.SaveChanges();
            }


            string moradaAntiga = _context.Autenticados
                            .Where(a => a.Username == utilizador)
                            .Select(a => a.Morada)
                            .FirstOrDefault();

            string NovaMorada = Input.NewMorada;

            var MoradaOLD = _context.Autenticados.FirstOrDefault(x => x.Morada == moradaAntiga);

            if (MoradaOLD != null)
            {
                MoradaOLD.Morada = NovaMorada;

                _context.SaveChanges();
            }


            DateTime dataAntiga = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.dtNasc)
                                        .FirstOrDefault();

            DateTime novaData = Input.DataNova;
            
            var DataOLD = _context.Autenticados.FirstOrDefault(x => x.dtNasc == dataAntiga);

            if (DataOLD != null)
            {
                DataOLD.dtNasc = novaData;

                _context.SaveChanges();
            }



            string FotoAntiga = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.FotoPerf)
                                        .FirstOrDefault();

            Foto = FotoAntiga;

            string novaFoto = Input.FotoNova;  

            var FotoOLD = _context.Autenticados.FirstOrDefault(x => x.FotoPerf == FotoAntiga);

            if (FotoOLD != null)
            {
                FotoOLD.FotoPerf = novaFoto;

                _context.SaveChanges();
            }
         

            StatusMessage = "O teu perfil foi atualizado com sucesso";
            return RedirectToPage("Perfil");
        }
    }
}
