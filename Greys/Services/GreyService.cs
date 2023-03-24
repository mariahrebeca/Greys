using System.Text.Json;
using Greys.Models;

namespace Greys.Services
{
    public class GreyService : IGreyService
    {
        private readonly IHttpContextAccessor _session;
        private readonly string greysFile = @"Data\personagens.json";
        private readonly string tiposFile = @"Data\tipos.json";

        public GreyService(IHttpContextAccessor session)
        {
            _session = session;
            PopularSessao();
        }

        public List<Grey> GetGrey()
        {
            PopularSessao();
            var personagens = JsonSerializer.Deserialize<List<Grey>>(_session.HttpContext.Session.GetString("Personagens"));
            return personagens;
        }

        public List<Tipo> GetTipos()
        {
            PopularSessao();
            var tipos = JsonSerializer.Deserialize<List<Tipo>>(_session.HttpContext.Session.GetString("Tipos"));
            return tipos;
        }

        public Grey GetGrey(int Numero)
        {
            var personagens = GetGrey();
            return personagens.Where(p => p.Numero == Numero).FirstOrDefault();
        }

        public GreyDto GetGreyDto()
        {
            var grey = new GreyDto()
            {
                Greys = GetGrey(),
                Tipos = GetTipos()
            };
            return grey;
        }

        public DetailsDto GetDetailedGrey(int Numero)
        {
            var personagens = GetGrey();
            var grey = new DetailsDto()
            {
                Current = personagens.Where(p => p.Numero == Numero).FirstOrDefault(),
                Prior = personagens.OrderByDescending(p => p.Numero).FirstOrDefault(p => p.Numero < Numero),
                Next = personagens.OrderBy(p => p.Numero).FirstOrDefault(p => p.Numero > Numero),
            };
            return grey;
        }

        public Tipo GetTipo(string Nome)
        {
            var tipos = GetTipos();
            return tipos.Where(t => t.Nome == Nome).FirstOrDefault();
        }

        private void PopularSessao()
        {
            if (string.IsNullOrEmpty(_session.HttpContext.Session.GetString("Tipos")))
            {
                _session.HttpContext.Session.SetString("Personagens", LerArquivo(greysFile));
                _session.HttpContext.Session.SetString("Tipos", LerArquivo(tiposFile));
            }
        }

        private string LerArquivo(string fileName)
        {
            using (StreamReader leitor = new StreamReader(fileName))
            {
                string dados = leitor.ReadToEnd();
                return dados;
            }

        }
    }
}