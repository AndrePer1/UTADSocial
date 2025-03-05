using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PL1_G1_1.Data;
using PL1_G1_1.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PL1_G1_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string searchTerm)
        {
            var grupo = _context.Grupos.FirstOrDefault(g => g.Nome == searchTerm);
            var pessoa = _context.Autenticados.FirstOrDefault(g => g.Username == searchTerm);
            ViewBag.SearchTerm = searchTerm;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (_context.Grupos.Any(g => g.Acesso == true && g.Nome == searchTerm))
                {
                    return RedirectToAction("Visualizar", "Grupos", new { id = grupo.IdGrupo });
                }
                else if (_context.Autenticados.Any(p => p.Username == searchTerm))
                {
                    return RedirectToAction("Perfil", "Areas/Identity/Pages/Account/Manage", new { id = pessoa.IdAutenticado });
                }
                else if((_context.Grupos.Any(g => g.Acesso == false && g.Nome == searchTerm)))
                {
                    TempData["Aviso"] = "Este Grupo é Privado. Solicite Acesso";
                }
                else
                {
                    TempData["Aviso"] = "Grupo não encontado";
                }
                
            }

            return View();
        }


        [Authorize]
        public IActionResult PedirAcesso(string searchTerm)
        {
            var grupo = _context.Grupos.FirstOrDefault(g => g.Nome == searchTerm);
            string utilizador = User.Identity.Name;
            int idDoUserAutenticado = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.IdAutenticado)
                                        .FirstOrDefault();

            var pedidoAcesso = _context.PedirAcessos
            .FirstOrDefault(p => p.IdGrupo == grupo.IdGrupo && p.IdAutenticado == idDoUserAutenticado);
            ViewBag.Pedidos = pedidoAcesso;
            if (pedidoAcesso == null)
            {

                var novoPedido = new PedirAcesso
                {
                    IdGrupo = grupo.IdGrupo,
                    IdAutenticado = idDoUserAutenticado,
                    DataPedido = DateTime.Now,
                    DataResposta = null,
                    Resultado = null,

                };

                _context.PedirAcessos.Add(novoPedido);
                _context.SaveChanges();

                //TempData["Mensagem"] = "Sua solicitação de acesso foi enviada aguarde aprovação.";

                return RedirectToAction("Index");
            }
            else if (pedidoAcesso != null && pedidoAcesso.Resultado == null)
            {
                //TempData["Mensagem"] = "Sua solicitação de acesso está pendente de aprovação.";

                return RedirectToAction("Index");
            }
            else if (pedidoAcesso != null && pedidoAcesso.Resultado == false)
            {
                //TempData["Mensagem"] = "A sua solicitação de acesso foi recusada.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Visualizar", "Grupos", new { id = grupo.IdGrupo });
            }
        }

        [Authorize]
        public IActionResult AderirGrupo(int idGrupo)
        {
            string nomeDono = User.Identity.Name;
            int idDoUserAutenticado = _context.Autenticados
                                        .Where(a => a.Username == nomeDono)
                                        .Select(a => a.IdAutenticado)
                                        .FirstOrDefault();
            Aderir novaAssociacao = new Aderir
            {
                IdAutenticado = idDoUserAutenticado,
                IdGrupo = idGrupo,
                DataAdesao = DateTime.Now
            };
            _context.Add(novaAssociacao);
            _context.SaveChanges();
            return RedirectToAction("Visualizar", "Grupos", new { id = idGrupo });
        }

        [Authorize]
        public async Task<IActionResult> SairGrupo(int idGrupo)
        {
            string nomeDono = User.Identity.Name;
            int idDoUserAutenticado = _context.Autenticados
                                        .Where(a => a.Username == nomeDono)
                                        .Select(a => a.IdAutenticado)
                                        .FirstOrDefault();
            var utilizador = _context.Aderirs
                        .FirstOrDefault(a => a.IdAutenticado == idDoUserAutenticado && a.IdGrupo == idGrupo);
            if (utilizador != null)
            {
                _context.Aderirs.Remove(utilizador);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("VerGrupos", "Grupos");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}