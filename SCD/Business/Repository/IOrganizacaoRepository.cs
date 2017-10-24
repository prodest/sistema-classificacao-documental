using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface IOrganizacaoRepository
    {
        Task<OrganizacaoModel> SearchByGuidAsync(Guid guidOrganizacao);
    }
}