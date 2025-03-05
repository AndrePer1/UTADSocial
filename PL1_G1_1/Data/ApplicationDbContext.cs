using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PL1_G1_1.Models;
using System.Text.RegularExpressions;

namespace PL1_G1_1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aderir> Aderirs { get; set; }

        public virtual DbSet<Autenticado> Autenticados { get; set; }

        public virtual DbSet<Comentar> Comentars { get; set; }

        public virtual DbSet<Ficheiro> Ficheiros { get; set; }

        public virtual DbSet<Grupo> Grupos { get; set; }

        public virtual DbSet<Midium> Midia { get; set; }

        public virtual DbSet<Partilha> Partilhas { get; set; }

        public virtual DbSet<PedirAcesso> PedirAcessos { get; set; }

        public virtual DbSet<Publicacao> Publicacaos { get; set; }

    }


}