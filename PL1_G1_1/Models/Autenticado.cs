using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PL1_G1_1.Models;

[Table("Autenticado")]
public partial class Autenticado
{
    [Key]
    [Column("id_autenticado")]
    public int IdAutenticado { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("Nome_completo")]
    [StringLength(255)]
    [Unicode(false)]
    public string NomeCompleto { get; set; } = null!;


    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Data de Nascimento")]
    public DateTime dtNasc { get; set; }

    [Required]
    public string Morada { get; set; }


    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; }


    [RegularExpression(@"^.+\.([jJ][pP][gG])$", ErrorMessage = "Só ficheiros .jpg !")]
    public string? FotoPerf { get; set; }

    [InverseProperty("IdAutenticadoNavigation")]
    public virtual ICollection<Aderir> Aderirs { get; set; } = new List<Aderir>();

    [InverseProperty("IdAutenticadoNavigation")]
    public virtual ICollection<Comentar> Comentars { get; set; } = new List<Comentar>();

    [InverseProperty("IdAutenticadoNavigation")]
    public virtual ICollection<Ficheiro> Ficheiros { get; set; } = new List<Ficheiro>();

    [InverseProperty("IdDonoNavigation")]
    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    [InverseProperty("IdAutenticadoNavigation")]
    public virtual ICollection<PedirAcesso> PedirAcessos { get; set; } = new List<PedirAcesso>();

    [InverseProperty("IdAutorNavigation")]
    public virtual ICollection<Publicacao> Publicacaos { get; set; } = new List<Publicacao>();
}
