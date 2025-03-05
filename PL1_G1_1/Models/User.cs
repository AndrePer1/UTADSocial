using System.ComponentModel.DataAnnotations;

namespace PL1_G1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {1} e {2} carateres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime Dt_Nasc { get; set; }


        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Password { get; set; }

        public string Morada { get; set; }

        public Boolean Estado { get; set; }

        public string? Foto { get; set; }


        public virtual ICollection<User> Grupo { get; set; }
    }
}
