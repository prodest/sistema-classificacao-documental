using Prodest.Scd.Business.Model;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface IOrganizacaoCore
    {
        OrganizacaoModel SearchAsync(string guid);
    }
}
