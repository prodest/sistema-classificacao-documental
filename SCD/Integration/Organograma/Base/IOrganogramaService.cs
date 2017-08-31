using Prodest.Scd.Integration.Organograma.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Integration.Organograma.Base
{
    public interface IOrganogramaService
    {
        Task<List<OrganogramaOrganizacao>> SearchAsync();
        Task<OrganogramaOrganizacao> SearchAsync(string guid);
        Task<OrganogramaOrganizacao> SearchPatriarcaAsync(string guid);
    }
}