﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PL1_G1_1.Data;
using PL1_G1_1.Models;

namespace PL1_G1_1.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;


        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        public string Morada { get; set; }

        public DateTime dtNascimento { get; set; }

        public string FotoPerfil { get; set; }

        public List<string> nomesGrupos { get; set; }

        public List<int> idGrupos { get; set; }


        
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
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }


        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            
            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            string utilizador = User.Identity.Name;

            int idUtilizador = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.IdAutenticado)
                                        .FirstOrDefault();

            string moradaDoUserAutenticado = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.Morada)
                                        .FirstOrDefault();

            DateTime dtNascDoUserAutenticado = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.dtNasc)
                                        .FirstOrDefault();

            string fotoDoUserAutenticado = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.FotoPerf)
                                        .FirstOrDefault();

            //foreach(var item in _context.Grupos)
            //{
                nomesGrupos = _context.Grupos
                                       .Where(a => a.IdDono == idUtilizador)
                                       .Select(a => a.Nome)
                                       .ToList();

                idGrupos = _context.Grupos
                                       .Where(a => a.IdDono == idUtilizador)
                                       .Select(a => a.IdGrupo)
                                       .ToList();
            //}


            Morada = moradaDoUserAutenticado;

            dtNascimento = dtNascDoUserAutenticado;

            FotoPerfil = fotoDoUserAutenticado;

            await LoadAsync(user);
            return Page();
        }

        private IActionResult Page(string moradaDoUserAutenticado, DateTime dtNascDoUserAutenticado)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                var viewmodel = new Model();

                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
