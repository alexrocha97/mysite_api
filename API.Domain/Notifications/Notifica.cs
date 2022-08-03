using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Notifications
{
    public class Notifica
    {
        
        public Notifica()
        {
            Notificacoes = new List<Notifica>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }
        [NotMapped]
        public string Mensagem { get; set; }
        [NotMapped]
        public List<Notifica> Notificacoes { get; set; }

        public bool ValidaPropriedadeString(string valor, string nomePropriedade)
        {
            if(string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica{
                    Mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade,
                });
                return false;
            }
            return true;
        }
    
        public bool ValidaPropriedadeDecimal(decimal valor, string nomePropriedade)
        {
            if(valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica{
                    Mensagem = "Valor deve ser maior que 0",
                    NomePropriedade = nomePropriedade,
                });
                return false;
            }
            return true;
        }
    }
}
