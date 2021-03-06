﻿using Prodest.Scd.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Business.Base
{
    public interface IDocumentoCore
    {
        Task<DocumentoModel> InsertAsync(DocumentoModel documentoModel);

        Task<DocumentoModel> SearchAsync(int id);

        Task<ICollection<DocumentoModel>> SearchByPlanoAsync(int idPlanoClassificacao);

        Task UpdateAsync(DocumentoModel documentoModel);

        Task DeleteAsync(int id);
    }
}