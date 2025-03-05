// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using PL1_G1_1.Data;
using PL1_G1_1.Models;
using System.Drawing;

namespace PL1_G1_1.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _he;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, ApplicationDbContext context,
            IHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _he = hostEnvironment;
        }

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
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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
            [Required(ErrorMessage = "Campo Obrigatório")]
            [Display(Name = "Nome de Utilizador")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Campo Obrigatório")]
            [Display(Name = "Nome completo")]
            public string nome_completo { get; set; }

            [Required(ErrorMessage = "Campo Obrigatório")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Campo Obrigatório")]
            [DataType(DataType.Date)]
            [Display(Name = "Data de Nascimento")]
            public DateTime dtNasc { get; set; }

            [Required(ErrorMessage = "Campo Obrigatório")]
            public string Morada { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Campo Obrigatório")]
            [StringLength(100, ErrorMessage = "A {0} tem de ter entre {2} e {1} carateres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "As passwords não condizem !")]
            public string ConfirmPassword { get; set; }


            [RegularExpression(@"^.+\.(jpg|png)$", ErrorMessage = "Só ficheiros .jpg ou .png!")]
            public string? FotoPerf { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(/*string returnUrl = null,*/ IFormFile FotoPerf)
        {
            //returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (Input.dtNasc > DateTime.Now)
            {
                ModelState.AddModelError("Data de Nascimento", "Data de Nascimento errada !");
            }

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    Autenticado newUser = new Autenticado();

                    newUser.Email = Input.Email;
                    newUser.Username = Input.Username;
                    newUser.dtNasc = Input.dtNasc;
                    newUser.NomeCompleto = Input.nome_completo;
                    newUser.Morada = Input.Morada;
                    
                    if(FotoPerf != null)
                    {
                        newUser.FotoPerf = FotoPerf.FileName;

                        string destino = Path.Combine(_he.ContentRootPath,
                           "wwwroot/Imagens/", Path.GetFileName(FotoPerf.FileName));

                        FileStream fs = new FileStream(destino, FileMode.Create);
                        FotoPerf.CopyTo(fs);
                        fs.Close();
                    }
                    else
                    { 
                        newUser.FotoPerf = "userNoFoto.png";
                    }


                    _context.Autenticados.Add(newUser);
                    _context.SaveChanges();

                    _logger.LogInformation("User created a new account with password.");


                    string returnUrl = null;

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Por favor confirma a tua conta no teu email");



                    string remetente = "utadSocial4@gmail.com";
                    string remetentePW = "uasdgbpipipqmflq";

                    MailMessage mensagem = new MailMessage();
                    mensagem.From = new MailAddress(remetente);
                    mensagem.Subject = "Confirmação de conta";
                    mensagem.To.Add(new MailAddress(Input.Email));
                    mensagem.Body = callbackUrl;
                    mensagem.IsBodyHtml = true;


                    //< html >< body > Por favor confirme a sua conta ao clicar aqui " " </ body ></ html >


                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(remetente, remetentePW),
                        EnableSsl = true,
                    };

                    smtpClient.Send(mensagem);



                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("Index");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
