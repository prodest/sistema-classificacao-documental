using Prodest.Scd.Business.Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface IDocumentoRepository
    {
        Task<DocumentoModel> AddAsync(DocumentoModel documentoModel);
        Task<DocumentoModel> SearchAsync(int id);
        Task<ICollection<DocumentoModel>> SearchByPlanoAsync(int idPlanoClassificacao);
        Task UpdateAsync(DocumentoModel documentoModel);
        Task RemoveAsync(int id);
    }
}