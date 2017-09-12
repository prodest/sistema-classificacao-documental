using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Common.Exceptions;
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
    public class UnitTestTipoDocumentalInsert
    {
        //private string _guidGees = Environment.GetEnvironmentVariable("GuidGEES");
        private TipoDocumentalCore _core;
        private List<TipoDocumentalModel> _tiposDocumentaisTestados = new List<TipoDocumentalModel>();

        [TestInitialize]
        public void Setup()
        {
            TipoDocumentalValidation tipoDocumentalValidation = new TipoDocumentalValidation(/*repositories*/);

            ScdRepositories repositories = new ScdRepositories();

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<BusinessProfileAutoMapper>();
            //});

            //IMapper mapper = Mapper.Instance;

            //OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            //OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new TipoDocumentalCore(tipoDocumentalValidation, repositories);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (TipoDocumentalModel tipoDocumental in _tiposDocumentaisTestados)
            {
                await _core.DeleteAsync(tipoDocumental.Id);
            }
        }

        [TestMethod]
        public async Task TipoDocumentalTestInsertNull()
        {
            bool ok = false;
            TipoDocumentalModel tipoDocumentalModel = null;

            try
            {
                tipoDocumentalModel = await _core.InsertAsync(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Tipo Documental não pode ser nulo.");
            }

            if (ok)
            {
                _tiposDocumentaisTestados.Add(tipoDocumentalModel);

                Assert.Fail("Não deveria ter inserido com objeto nulo.");
            }
        }

        #region Descrição
        [TestMethod]
        public async Task TipoDocumentalTestInsertWithDescricaoNull()
        {
            TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel();

            bool ok = false;

            try
            {
                tipoDocumentalModel = await _core.InsertAsync(tipoDocumentalModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
            {
                _tiposDocumentaisTestados.Add(tipoDocumentalModel);

                Assert.Fail("Não deveria ter inserido com descrição nula.");
            }
        }

        //[TestMethod]
        //public async Task TipoDocumentalTestInsertWithDescricaoEmpty()
        //{
        //    TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel { Descricao = "" };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(tipoDocumentalModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com descrição vazia.");
        //}

        //[TestMethod]
        //public async Task TipoDocumentalTestInsertWithDescricaoTrimEmpty()
        //{
        //    TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel { Descricao = " " };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(tipoDocumentalModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com descrição vazia.");
        //}
        #endregion

        //#region Id
        //[TestMethod]
        //public async Task TipoDocumentalTestInsertWithInvalidInsertId()
        //{
        //    TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel { Id = 1, Descricao = "Teste", Organizacao = new OrganizacaoModel { GuidOrganizacao = new Guid(_guidGees) } };

        //    bool ok = false;

        //    try
        //    {
        //        await _core.InsertAsync(tipoDocumentalModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(ScdException));

        //        Assert.AreEqual(ex.Message, "O id não deve ser preenchido.");
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter inserido com o id inválido para inserção.");
        //}
        //#endregion

        //[TestMethod]
        //public async Task TipoDocumentalTestInsert()
        //{
        //    string descricao = "Descrição Teste";
        //    Guid guidOrganizacao = new Guid(_guidGees);

        //    TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel { Descricao = descricao, Organizacao = new OrganizacaoModel { GuidOrganizacao = guidOrganizacao } };

        //    tipoDocumentalModel = await _core.InsertAsync(tipoDocumentalModel);

        //    Assert.IsTrue(tipoDocumentalModel.Id > 0);
        //    Assert.AreEqual(tipoDocumentalModel.Descricao, descricao);
        //    Assert.AreEqual(tipoDocumentalModel.Ativo, true);
        //    Assert.IsFalse(tipoDocumentalModel.Organizacao == null);
        //    Assert.AreEqual(tipoDocumentalModel.Organizacao.GuidOrganizacao, guidOrganizacao);
        //}
    }
}