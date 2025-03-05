using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PL1_G1_1.Models;

[PrimaryKey("IdAutenticado", "IdGrupo")]
[Table("Pedir_acesso")]
public partial class PedirAcesso
{
    [Key]
    [Column("Id_autenticado")]
    public int IdAutenticado { get; set; }

    [Key]
    [Column("Id_grupo")]
    public int IdGrupo { get; set; }

    [Column("Data_resposta", TypeName = "smalldatetime")]
    public DateTime? DataResposta { get; set; }

    [Column("Data_pedido", TypeName = "smalldatetime")]
    public DateTime DataPedido { get; set; }

    [Column("resultado")]
    public bool? Resultado { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Mensagem { get; set; }

    [ForeignKey("IdAutenticado")]
    [InverseProperty("PedirAcessos")]
    public virtual Autenticado IdAutenticadoNavigation { get; set; } = null!;

    [ForeignKey("IdGrupo")]
    [InverseProperty("PedirAcessos")]
    public virtual Grupo IdGrupoNavigation { get; set; } = null!;
}
