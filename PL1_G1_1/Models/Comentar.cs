using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PL1_G1_1.Models;

[PrimaryKey("IdAutenticado", "IdPublicacao", "DataComentario")]
[Table("Comentar")]
public partial class Comentar
{
    [Key]
    [Column("Id_autenticado")]
    public int IdAutenticado { get; set; }

    [Key]
    [Column("Id_publicacao")]
    public int IdPublicacao { get; set; }

    [Key]
    [Column("Data_comentario", TypeName = "smalldatetime")]
    public DateTime DataComentario { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string Conteudo { get; set; } = null!;

    [ForeignKey("IdAutenticado")]
    [InverseProperty("Comentars")]
    public virtual Autenticado IdAutenticadoNavigation { get; set; } = null!;

    [ForeignKey("IdPublicacao")]
    [InverseProperty("Comentars")]
    public virtual Publicacao IdPublicacaoNavigation { get; set; } = null!;
}
