using Prodest.Scd.Business.Model;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface IOrganizacaoCore
    {
        OrganizacaoModel SearchAsync(Guid guidOrganizacao);
        OrganizacaoModel SearchAsync(int id, Guid guidOrganizacao);
    }
}
