using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PL1_G1_1.Models;

[Table("Publicacao")]
public partial class Publicacao
{
    [Key]
    [Column("Id_publicacao")]
    public int IdPublicacao { get; set; }

    [Column("Id_autor")]
    public int IdAutor { get; set; }

    [Column("Data_publicacao", TypeName = "smalldatetime")]
    public DateTime DataPublicacao { get; set; } = DateTime.Now;

    [Column("Tipo_publicacao")]
    [StringLength(7)]
    [Unicode(false)]
    public string TipoPublicacao { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    [Required(ErrorMessage = "É necessário inserir conteúdo para fazer uma publicação.")]
    public string Conteudo { get; set; } = null!;

    [InverseProperty("IdPublicacaoNavigation")]
    public virtual ICollection<Comentar> Comentars { get; set; } = new List<Comentar>();

    [ForeignKey("IdAutor")]
    [InverseProperty("Publicacaos")]
    public virtual Autenticado IdAutorNavigation { get; set; } = null!;

    [InverseProperty("IdPublicacoNavigation")]
    public virtual ICollection<Midium> Midia { get; set; } = new List<Midium>();

    [ForeignKey("IdPublicacao")]
    [InverseProperty("IdPublicacaos")]
    public virtual ICollection<Grupo> IdGrupos { get; set; } = new List<Grupo>();
}
