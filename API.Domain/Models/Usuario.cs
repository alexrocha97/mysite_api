using System.ComponentModel.DataAnnotations.Schema;
using API.Domain.Enums;

namespace API.Domain.Models
{
    [NotMapped]
    public class Usuario
    {
        public string Email {get; set;}
        public int Idade { get; set; }
        public string Celular { get; set; }
        public string Tipo { get; set; }
    }
}
