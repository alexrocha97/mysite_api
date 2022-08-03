using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Domain.Notifications;

namespace API.Domain.Models
{
    [Table("TB_NOTICIA")]
    public class Noticia : Notifica
    {
        [Column("NTC_ID")]
        public int Id { get; set; }
        [Column("NTC_TITULO")]
        [MaxLength(255)]
        public string Titulo { get; set; }
        [Column("NTC_INFORMACAO")]
        [MaxLength(255)]
        public string Informacao { get; set; }
        [Column("NTC_ATIVO")]
        public bool Ativo { get; set; }
        [Column("NTC_DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }
        [Column("NTC_DATA_ALTERACAO")]
        public DateTime DataAlteracao { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId {get; set;}
        public virtual ApplicationUser ApplicationUser {get; set;}

    }
}
