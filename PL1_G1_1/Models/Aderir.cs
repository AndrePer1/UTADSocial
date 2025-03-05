using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PL1_G1_1.Models;

[PrimaryKey("IdAutenticado", "IdGrupo")]
[Table("Aderir")]
public partial class Aderir
{
    [Key]
    [Column("Id_autenticado")]
    public int IdAutenticado { get; set; }

    [Key]
    [Column("Id_grupo")]
    public int IdGrupo { get; set; }

    [Column("Data_adesao", TypeName = "smalldatetime")]
    public DateTime DataAdesao { get; set; }

    [ForeignKey("IdAutenticado")]
    [InverseProperty("Aderirs")]
    public virtual Autenticado IdAutenticadoNavigation { get; set; } = null!;

    [ForeignKey("IdGrupo")]
    [InverseProperty("Aderirs")]
    public virtual Grupo IdGrupoNavigation { get; set; } = null!;
}
