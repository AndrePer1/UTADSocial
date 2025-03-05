using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PL1_G1_1.Models;

public partial class Ficheiro
{
    [Key]
    [Column("Id_ficheiro")]
    public int IdFicheiro { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [Column("id_autenticado")]
    public int? IdAutenticado { get; set; }

    [ForeignKey("IdAutenticado")]
    [InverseProperty("Ficheiros")]
    public virtual Autenticado? IdAutenticadoNavigation { get; set; }

    [InverseProperty("IdFicheiroNavigation")]
    public virtual ICollection<Partilha> Partilhas { get; set; } = new List<Partilha>();
}
