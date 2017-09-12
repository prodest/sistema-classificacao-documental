using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface ITipoDocumentalCore
    {
        int Count(Guid guidOrganizacao);

        Task DeleteAsync(int id);

        Task<TipoDocumentalModel> InsertAsync(TipoDocumentalModel tipoDocumental);

        TipoDocumentalModel Search(int id);

        List<TipoDocumentalModel> Search(Guid guidOrganizacao, int page, int count);

        Task UpdateAsync(TipoDocumentalModel tipoDocumental);
    }
}