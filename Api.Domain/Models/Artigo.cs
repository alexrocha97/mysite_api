namespace Api.Domain.Models
{
    public class Artigo
    {
        public Artigo(string nome, string titulo, string texto, int curtidas)
        {
            this.nome = nome;
            this.titulo = titulo;
            this.texto = texto;
            this.curtidas = curtidas;
        }

        public string nome {get; set;}
        public string titulo {get; set;}
        public string texto {get; set;}
        public int curtidas {get; set;}
    }
}
