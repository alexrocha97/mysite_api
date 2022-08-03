using System.ComponentModel.DataAnnotations.Schema;
using API.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace API.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USR_IDADE")]
        public int Idade { get; set; }
        [Column("USR_CELULAR")]
        public string Celular { get; set; }
        [Column("USR_TIPO")]
        public TipoUsuario? Tipo { get; set; }
    }
}
