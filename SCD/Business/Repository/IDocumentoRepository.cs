using Prodest.Scd.Business.Model;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Repository
{
    public interface IDocumentoRepository
    {
        Task<DocumentoModel> AddAsync(DocumentoModel documentoModel);
        Task<DocumentoModel> SearchAsync(int id);
        Task<int> CountSigilo(int id);
        Task<int> CountTemporalidade(int id);
        Task UpdateAsync(DocumentoModel documentoModel);
        Task RemoveAsync(int id);
    }
}