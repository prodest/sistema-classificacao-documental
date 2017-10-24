using Prodest.Scd.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface ITipoDocumentalRepository
    {
        Task<TipoDocumentalModel> AddAsync(TipoDocumentalModel tipoDocumentalModel);
        Task<TipoDocumentalModel> SearchAsync(int id);
        Task<ICollection<TipoDocumentalModel>> SearchByOrganizacaoAsync(Guid guidOrganizacao, int page, int count);
        Task<int> CountByOrganizacaoAsync(Guid guidOrganizacao);
        Task UpdateAsync(TipoDocumentalModel tipoDocumentalModel);
        Task RemoveAsync(int id);
    }
}