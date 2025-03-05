using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PL1_G1_1.Models;

[Table("Grupo")]
public partial class Grupo
{
    [Key]
    [Column("Id_grupo")]
    public int IdGrupo { get; set; }

    [Column("Id_dono")]
    public int IdDono { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    public bool Acesso { get; set; }

    [Column("Data_criacao", TypeName = "smalldatetime")]
    public DateTime DataCriacao { get; set; }

    [InverseProperty("IdGrupoNavigation")]
    public virtual ICollection<Aderir> Aderirs{ get; set; } = new List<Aderir>();

    [ForeignKey("IdDono")]
    [InverseProperty("Grupos")]
    public virtual Autenticado IdDonoNavigation { get; set; } = null!;

    [InverseProperty("IdGrupoNavigation")]
    public virtual ICollection<Partilha> Partilhas { get; set; } = new List<Partilha>();

    [InverseProperty("IdGrupoNavigation")]
    public virtual ICollection<PedirAcesso> PedirAcessos { get; set; } = new List<PedirAcesso>();

    [ForeignKey("IdGrupo")]
    [InverseProperty("IdGrupos")]
    public virtual ICollection<Publicacao> IdPublicacaos { get; set; } = new List<Publicacao>();
}
