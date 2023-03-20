using Greys.Models;

namespace Greys.Services
{
    public interface IGreyService
    {
        List<Grey> GetGrey();
        List<Tipo> GetTipos();
        Grey GetGrey(int Numero);
        GreyDto GetGreyDto();
        DetailsDto GetDetailedGrey(int Numero);
        Tipo GetTipos(string Nome);
    }
}