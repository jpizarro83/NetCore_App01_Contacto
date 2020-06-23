using System;
using System.ComponentModel.DataAnnotations;

namespace contactos.Models
{
    public class UserDto
    {
        [Key]
        [Required]
        [Display(Name="Username")]
        [StringLength(20, ErrorMessage = "El valor para {0] debe contener al menos {2} y máximo {1} caracteres", MinimumLength=6)]
        public string username {get; set;}
        
        public string password {get; set;}

        public string email {get; set;}

        [DataType(DataType.Date)]
        public DateTime FechaCreado {get; set;}
    }
}