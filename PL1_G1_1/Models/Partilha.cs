using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PL1_G1_1.Models;

[PrimaryKey("IdGrupo", "IdFicheiro")]
[Table("Partilha")]
public partial class Partilha
{
    [Key]
    [Column("Id_grupo")]
    public int IdGrupo { get; set; }

    [Key]
    [Column("id_ficheiro")]
    public int IdFicheiro { get; set; }

    [Column("Data_partilha", TypeName = "date")]
    public DateTime DataPartilha { get; set; }

    [ForeignKey("IdFicheiro")]
    [InverseProperty("Partilhas")]
    public virtual Ficheiro IdFicheiroNavigation { get; set; } = null!;

    [ForeignKey("IdGrupo")]
    [InverseProperty("Partilhas")]
    public virtual Grupo IdGrupoNavigation { get; set; } = null!;
}
