using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PL1_G1_1.Data;
using PL1_G1_1.Models;

namespace PL1_G1_1.Controllers
{
    public class PublicacoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _he;

        public PublicacoesController(ApplicationDbContext context, IHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Publicacoes
        public async Task<IActionResult> VerPublicacoes()
        {
            var applicationDbContext = _context.Publicacaos
        .Include(p => p.IdAutorNavigation)
        .Include(p => p.Midia) 
        .ToListAsync(); // ToListAsync para obter uma lista assíncrona

            return View(await applicationDbContext);
        }

        // GET: Publicacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publicacaos == null)
            {
                return NotFound();
            }

            var publicacao = await _context.Publicacaos
                .Include(p => p.IdAutorNavigation)
                .Include(p => p.Midia)

                .Include(p => p.IdAutorNavigation)
                .FirstOrDefaultAsync(m => m.IdPublicacao == id);
            if (publicacao == null)
            {
                return NotFound();
            }

            return View(publicacao);
        }
        // GET: Publicacoes/Create
        [Authorize]
        public IActionResult Create()
        {
            string nomeDono = User.Identity.Name;
            int idUserAutenticado = _context.Autenticados.Where(a => a.Username == nomeDono).Select(a => a.IdAutenticado).FirstOrDefault();

            ViewData["IdAutor"] = new SelectList(_context.Autenticados, "IdAutenticado");
            return View();
        }

        // POST: Publicacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoPublicacao,Conteudo")] Publicacao publicacao, IFormFile midia, Midium midium)
        {
            string nomeDono = User.Identity.Name;
            int idUserAutenticado = _context.Autenticados
                .Where(a => a.Username == nomeDono)
                .Select(a => a.IdAutenticado)
                .FirstOrDefault();

            publicacao.IdAutor = idUserAutenticado;
            publicacao.DataPublicacao = DateTime.Now;


            if (midia != null)
            {
                string destination = Path.Combine(_he.ContentRootPath, "wwwroot/Midia/", Path.GetFileName(midia.FileName));

                FileStream fs = new FileStream(destination, FileMode.Create);
                midia.CopyTo(fs);
                fs.Close();
                // Associa o arquivo à publicação
                var Midia = new Midium
                {
                    Nome = midia.FileName,
                    // Outros atributos relacionados ao arquivo, como tipo e tamanho, podem ser definidos aqui
                };

                if (publicacao.Midia == null)
                {
                    publicacao.Midia = new List<Midium>();
                }

                publicacao.Midia.Add(Midia);
            }

            if (!ModelState.IsValid)
            {
                _context.Add(publicacao);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(VerPublicacoes));
            }

            return View(publicacao);
        }

        // GET: Publicacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacao = await _context.Publicacaos.FindAsync(id);
            if (publicacao == null)
            {
                return NotFound();
            }
            ViewData["IdAutor"] = new SelectList(_context.Autenticados, "IdAutenticado", "Morada", publicacao.IdAutor);
            return View(publicacao);
        }

        // POST: Publicacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPublicacao,IdAutor,DataPublicacao,TipoPublicacao,Conteudo")] Publicacao publicacao, IFormFile midia, Midium midium)
        {
            if (id != publicacao.IdPublicacao)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    // Carrega a entidade original do banco de dados
                    var originalPublicacao = await _context.Publicacaos
                        .Include(p => p.Midia)
                        .FirstOrDefaultAsync(p => p.IdPublicacao == id);

                    if (originalPublicacao == null)
                    {
                        return NotFound();
                    }

                    // Manter o ID do autor
                    publicacao.IdAutor = originalPublicacao.IdAutor;

                    // Atualiza as propriedades que podem ser alteradas
                    originalPublicacao.DataPublicacao = publicacao.DataPublicacao;
                    originalPublicacao.TipoPublicacao = publicacao.TipoPublicacao;
                    originalPublicacao.Conteudo = publicacao.Conteudo;

                    // Remove mídias antigas (caso haja) antes de adicionar a nova mídia
                    _context.Midia.RemoveRange(originalPublicacao.Midia);

                    // Verifica se foi fornecida uma nova mídia
                    if (midia != null)
                    {
                        var novaMidia = new Midium
                        {
                            Nome = midia.FileName,
                            // Outros atributos relacionados ao arquivo, como tipo e tamanho, podem ser definidos aqui
                        };

                        // Adiciona a nova mídia
                        originalPublicacao.Midia.Add(novaMidia);

                        // Salva a mídia no sistema de arquivos
                        string destination = Path.Combine(_he.ContentRootPath, "wwwroot/Midia/", Path.GetFileName(midia.FileName));
                        using (FileStream fs = new FileStream(destination, FileMode.Create))
                        {
                            await midia.CopyToAsync(fs);
                        }
                    }

                    _context.Update(originalPublicacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicacaoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(VerPublicacoes));
            }

            ViewData["IdAutor"] = new SelectList(_context.Autenticados, "IdAutenticado", "Morada", publicacao.IdAutor);
            return View(publicacao);
        }


        // GET: Publicacoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //Incluir as entidades relacionadas para garantir a remoção
            var publicacao = await _context.Publicacaos
                .Include(p => p.Midia) 
                .FirstOrDefaultAsync(p => p.IdPublicacao == id);

            if (publicacao == null)
            {
                return NotFound();
            }

            try
            {
                // Remover as midia antes de remover a publicaçao
                _context.Midia.RemoveRange(publicacao.Midia);
                _context.Publicacaos.Remove(publicacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(VerPublicacoes));
            }
            catch (Exception ex)
            {
                // Lidar com exceções, se necessário
                return RedirectToAction(nameof(VerPublicacoes));
            }
        }


        private bool PublicacaoExists(int id)
        {
          return (_context.Publicacaos?.Any(e => e.IdPublicacao == id)).GetValueOrDefault();
        }
    }
}
