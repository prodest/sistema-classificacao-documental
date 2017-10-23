using Prodest.Scd.Business.Model;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface IOrganizacaoCore
    {
        Task<OrganizacaoModel> SearchAsync(Guid guidOrganizacao);
    }
}
