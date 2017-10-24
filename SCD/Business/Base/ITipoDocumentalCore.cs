using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface ITipoDocumentalCore
    {
        Task<int> CountAsync(Guid guidOrganizacao);

        Task DeleteAsync(int id);

        Task<TipoDocumentalModel> InsertAsync(TipoDocumentalModel tipoDocumental);

        Task<TipoDocumentalModel> SearchAsync(int id);

        Task<ICollection<TipoDocumentalModel>> SearchAsync(Guid guidOrganizacao, int page, int count);

        Task UpdateAsync(TipoDocumentalModel tipoDocumental);
    }
}