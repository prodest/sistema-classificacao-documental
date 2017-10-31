using Prodest.Scd.Presentation.ViewModel;
using System.Threading.Tasks;

namespace Prodest.Scd.Presentation.Base
{
    public interface IDocumentoService
    {
        Task<DocumentoViewModel> Delete(int id);
        Task<DocumentoViewModel> Edit(int id);
        Task<DocumentoViewModel> Update(DocumentoEntidade entidade);
        Task<DocumentoViewModel> Create(DocumentoEntidade entidade);
        Task<DocumentoViewModel> New(int IdItemPlanoClassificacao);

    }
}