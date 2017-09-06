using Prodest.Scd.Integration.Organograma.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Integration.Organograma.Base
{
    public interface IOrganogramaService
    {
        Task<List<OrganogramaOrganizacao>> SearchAsync();
        Task<OrganogramaOrganizacao> SearchAsync(Guid guidOrganizacao);
        Task<OrganogramaOrganizacao> SearchPatriarcaAsync(Guid guidOrganizacao);
    }
}