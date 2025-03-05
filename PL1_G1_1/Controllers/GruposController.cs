using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using PL1_G1_1.Data;
using PL1_G1_1.Models;

namespace PL1_G1_1.Controllers
{
    public class GruposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GruposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grupos
        public async Task<IActionResult> VerGrupos()
        {
            string nomeDono = User.Identity.Name;
            int idDoUserAutenticado = _context.Autenticados
                                        .Where(a => a.Username == nomeDono)
                                        .Select(a => a.IdAutenticado)
                                        .FirstOrDefault();

            int idAutenticado =  idDoUserAutenticado;
            ViewBag.idAutenticado = idAutenticado;

            var gruposqueparticipoTask = _context.Aderirs.Where(a => a.IdAutenticado == idAutenticado)
                                                    .Select(a => a.IdGrupo).ToListAsync();

            var gruposqueparticipo = await gruposqueparticipoTask;

            var grupos = await _context.Grupos
                                .Where(g => gruposqueparticipo.Contains(g.IdGrupo))
                                .ToListAsync();
            return View(grupos);
        }

        public IActionResult VerPedidosDeAcesso(int? id)
        {
            var pedidos = _context.PedirAcessos.Where(g => g.IdGrupo == id && g.Resultado == null).ToList();

            var ids = _context.PedirAcessos.Where(a => a.IdGrupo == id).Select(a => a.IdAutenticado);
            var nome = _context.Autenticados.Where(a => ids.Contains(a.IdAutenticado)).Select(a => a.NomeCompleto);

            ViewBag.Nome = nome;
            return View(pedidos);
        }
        public async Task<IActionResult> AceitarPedido(int id, int idgrupo)
        {
            var pedido = await _context.PedirAcessos.FindAsync(id, idgrupo);

                pedido.DataResposta = DateTime.Now;
                pedido.Resultado = true;
                _context.Update(pedido);
                Aderir novo = new Aderir
                {
                    IdAutenticado = id,
                    IdGrupo = idgrupo,
                    DataAdesao = DateTime.Now
                };
                _context.Add(novo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(VerGrupos));
        }
        public async Task<IActionResult> RecusarPedido(int id, int idgrupo)
        {
            var pedido = await _context.PedirAcessos.FindAsync(id,idgrupo);
            pedido.DataResposta = DateTime.Now;
            pedido.Resultado = false;
            _context.Update(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VerGrupos));
        }
        public async Task<IActionResult> EliminarParticipante(string nome, int idgrupo)
        {
            var participantes= _context.Autenticados.Where(a=>a.NomeCompleto==nome).Select(a=>a.IdAutenticado).FirstOrDefault();

            var id = _context.Aderirs.Where(a => a.IdAutenticado == participantes).Select(a => a.IdAutenticado).FirstOrDefault();

            var participante = await _context.Aderirs.FindAsync(id, idgrupo);

            _context.Aderirs.Remove(participante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VerGrupos));
        }

        // GET: Grupos/Details/5
        public async Task<IActionResult> Visualizar(int? id)
        {
            var grupoEncontrado = _context.Grupos
                          .FirstOrDefault(g=> g.IdGrupo == id);

            string utilizador = User.Identity.Name;
            int idDoUserAutenticado = _context.Autenticados
                                        .Where(a => a.Username == utilizador)
                                        .Select(a => a.IdAutenticado)
                                        .FirstOrDefault();

            bool usuarioJaEstaNoGrupo = _context.Aderirs
                    .Any(a => a.IdAutenticado == idDoUserAutenticado && a.IdGrupo == grupoEncontrado.IdGrupo);

            int participantes = _context.Aderirs.Where(a=>a.IdGrupo == grupoEncontrado.IdGrupo).Count();

            var ids = _context.Aderirs.Where(a => a.IdGrupo == grupoEncontrado.IdGrupo).Select(a => a.IdAutenticado);
            var nome = _context.Autenticados.Where(a=> ids.Contains(a.IdAutenticado)).Select(a=>a.NomeCompleto);

            

            ViewBag.Nome = nome;
            ViewBag.Participantes = participantes;
            ViewBag.UsuarioEstaNoGrupo = usuarioJaEstaNoGrupo;
            ViewBag.IdGrupo = grupoEncontrado.IdGrupo;

            if (id == null || _context.Grupos == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .Include(g => g.IdDonoNavigation)
                .FirstOrDefaultAsync(m => m.IdGrupo == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // GET: Grupos/Create
        [Authorize]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGrupo,IdDono,Nome,Descricao,Acesso,DataCriacao")] Grupo grupo)
        {
 
            string nomeDono = User.Identity.Name;
            int idDoUserAutenticado = _context.Autenticados
                                        .Where(a => a.Username == nomeDono)
                                        .Select(a => a.IdAutenticado)
                                        .FirstOrDefault();

            grupo.IdDono = idDoUserAutenticado;
            grupo.DataCriacao = DateTime.Now;
            
            if (grupo.Nome == null)
            {
                TempData["Erro"] = "O grupo tem que ter um nome!";
                return RedirectToAction(nameof(Create));
            }

            if (_context.Grupos.Any(a => a.Nome == grupo.Nome))
            {
                TempData["Erro"] = "Esse grupo já existe!";
                return RedirectToAction(nameof(Create));
            }

            if (!ModelState.IsValid)
            {
                _context.Add(grupo);
                await _context.SaveChangesAsync();
                Aderir aderir = new Aderir()
                {
                    IdAutenticado = idDoUserAutenticado,
                    IdGrupo = grupo.IdGrupo,
                    DataAdesao = DateTime.Now
                };
                _context.Add(aderir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(VerGrupos));
            }
            return View(grupo);
        }

        // GET: Grupos/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            var grupoEncontrado = _context.Grupos
                         .FirstOrDefault(g => g.IdGrupo == id);

            int participantes = _context.Aderirs.Where(a => a.IdGrupo == grupoEncontrado.IdGrupo).Count();

            var ids = _context.Aderirs.Where(a => a.IdGrupo == grupoEncontrado.IdGrupo).Select(a => a.IdAutenticado);
            var nome = _context.Autenticados.Where(a => ids.Contains(a.IdAutenticado)).Select(a => a.NomeCompleto);

            ViewBag.Nome = nome;
            ViewBag.Participantes = participantes;
            if (id == null || _context.Grupos == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }
            ViewData["IdDono"] = new SelectList(_context.Autenticados, "IdAutenticado", "Morada", grupo.IdDono);
            return View(grupo);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdGrupo,IdDono,Nome,Descricao,Acesso,DataCriacao")] Grupo grupo)
        {

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.IdGrupo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(VerGrupos));
            }
            ViewData["IdDono"] = new SelectList(_context.Autenticados, "IdAutenticado", "Morada", grupo.IdDono);
            return View(grupo);
        }

        // GET: Grupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grupos == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .Include(g => g.IdDonoNavigation)
                .FirstOrDefaultAsync(m => m.IdGrupo == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var grupo = await _context.Grupos
                .Include(p => p.Aderirs)
                .Include(p=> p.PedirAcessos)
                .FirstOrDefaultAsync(p => p.IdGrupo == id);

            if (_context.Grupos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Grupos'  is null.");
            }
            if (grupo != null)
            {
                _context.Aderirs.RemoveRange(grupo.Aderirs);
                _context.PedirAcessos.RemoveRange(grupo.PedirAcessos);
                _context.Grupos.Remove(grupo);

            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VerGrupos));
        }

        private bool GrupoExists(int id)
        {
          return (_context.Grupos?.Any(e => e.IdGrupo == id)).GetValueOrDefault();
        }


    }
}
