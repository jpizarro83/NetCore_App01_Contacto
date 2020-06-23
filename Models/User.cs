using System;
using System.ComponentModel.DataAnnotations;

namespace contactos.Models
{
    public class User
    {
        [Key]
        [Required]
        [Display(Name="Username")]
        [StringLength(20, ErrorMessage = "El valor para {0} debe contener al menos {2} y máximo {1} caracteres de longitud.", MinimumLength = 6)]
        public string username { get; set; }
		public DateTime fechacreado { get; set; }
		public string email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}