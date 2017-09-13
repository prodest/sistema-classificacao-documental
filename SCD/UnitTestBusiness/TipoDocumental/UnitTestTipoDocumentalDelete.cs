using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.TipoDocumental
{
    [TestClass]
    public class UnitTestTipoDocumentalDelete
    {
        private TipoDocumentalCore _core;
        private List<TipoDocumentalModel> _tiposDocumentaisTestados = new List<TipoDocumentalModel>();
        ScdRepositories _repositories = new ScdRepositories();

        [TestInitialize]
        public void Setup()
        {
            TipoDocumentalValidation tipoDocumentalValidation = new TipoDocumentalValidation();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation, mapper);

            _core = new TipoDocumentalCore(tipoDocumentalValidation, _repositories, organizacaoCore, mapper);
        }

        #region Id
        [TestMethod]
        public async Task TipoDocumentalTestDeleteWithInvalidId()
        {
            bool ok = false;

            try
            {
                await _core.DeleteAsync(default(int));

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O id não pode ser nulo ou vazio.");
            }

            if (ok)
                Assert.Fail("Não deveria ter excluído com id zero.");
        }
        #endregion

        [TestMethod]
        public async Task TipoDocumentalTestDeleteWithIdNonexistentOnDataBase()
        {
            bool ok = false;

            try
            {
                await _core.DeleteAsync(-1);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Tipo Documental não encontrado.");
            }

            if (ok)
                Assert.Fail("Não deveria ter pesquisado com id inexistente na base de dados.");
        }

        //TODO: Após a implementação dos CRUDs de Itens de Plnao de Classificacação fazer os testes de remoção de tipo documental com itens associados

        [TestMethod]
        public async Task TipoDocumentalTestDelete()
        {
            Persistence.Model.TipoDocumental tipoDocumental = new Persistence.Model.TipoDocumental { Descricao = "Tipo Documental Teste", Ativo = true, IdOrganizacao = 1 };

            tipoDocumental = await _repositories.TiposDocumentais.AddAsync(tipoDocumental);

            await _repositories.UnitOfWork.SaveAsync();

            await _core.DeleteAsync(tipoDocumental.Id);

            tipoDocumental = _repositories.TiposDocumentais.SingleOrDefault(td => td.Id == tipoDocumental.Id);

            if (tipoDocumental != null)
                Assert.Fail("O repositório não deveria conter um Tipo Documental excluído.");
        }
    }
}