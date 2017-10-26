﻿using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Validation;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class DocumentoCore : IDocumentoCore
    {
        private IDocumentoRepository _documentos;

        private DocumentoValidation _validation;

        public DocumentoCore(IDocumentoRepository documentos, DocumentoValidation validation)
        {
            _documentos = documentos;

            _validation = validation;
        }

        public async Task<DocumentoModel> InsertAsync(DocumentoModel documentoModel)
        {
            await _validation.BasicValid(documentoModel);

            _validation.IdInsertValid(documentoModel.Id);

            //TODO: Verificar se o usuário pode inserir quando o sistema conseguir obter organzação do usuário
            documentoModel = await _documentos.AddAsync(documentoModel);

            return documentoModel;
        }

        public async Task<DocumentoModel> SearchAsync(int id)
        {
            _validation.IdValid(id);

            DocumentoModel documentoModel = await _documentos.SearchAsync(id);

            _validation.Found(documentoModel);

            return documentoModel;
        }

        public async Task UpdateAsync(DocumentoModel documentoModel)
        {
            await _validation.Valid(documentoModel);

            DocumentoModel documentoModelOld = await _documentos.SearchAsync(documentoModel.Id);

            _validation.Found(documentoModelOld);

            await _validation.PlanoClassificacaoEquals(documentoModel, documentoModelOld);

            await _validation.CanUpdate(documentoModelOld);

            await _documentos.UpdateAsync(documentoModel);
        }

        public async Task DeleteAsync(int id)
        {
            DocumentoModel documentoModel = await _documentos.SearchAsync(id);

            _validation.Found(documentoModel);

            await _validation.CanDelete(documentoModel);

            await _documentos.RemoveAsync(documentoModel.Id);
        }
    }
}