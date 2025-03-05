using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PL1_G1_1.Models;

public partial class Midium
{
    [Key]
    [Column("Id_midia")]
    public int IdMidia { get; set; }

    [StringLength(60)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [Column("Id_publicaco")]
    public int? IdPublicaco { get; set; }

    [ForeignKey("IdPublicaco")]
    [InverseProperty("Midia")]
    public virtual Publicacao? IdPublicacoNavigation { get; set; }
}
