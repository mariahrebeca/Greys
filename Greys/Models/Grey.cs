namespace Greys.Models
{
    public class Grey
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<string> Tipo { get; set; }
        public string Imagem { get; set; }
        public Grey()
        {
            Tipo = new List<string>();
        }
    }
}