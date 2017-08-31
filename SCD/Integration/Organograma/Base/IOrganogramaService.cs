using Prodest.Scd.Integration.Organograma.Model;
using System.Threading.Tasks;

namespace Prodest.Scd.Integration.Organograma.Base
{
    public interface IOrganogramaService
    {
        Task<OrganogramaOrganizacao> SearchAsync(string guid);
        Task<OrganogramaOrganizacao> SearchPatriarcaAsync(string guid);
    }
}