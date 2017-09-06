using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Common.Exceptions;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Repository;
using System;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.NivelClassificacao
{
    [TestClass]
    public class UnitTestNivelClassificacaoUpdate
    {
        private string _guidGees = Environment.GetEnvironmentVariable("GuidGEES");
        private NivelClassificacaoCore _core;
        private NivelClassificacaoModel _nivelClassificacaoModel;

        [TestInitialize]
        public async Task Setup()
        {
            ScdRepositories repositories = new ScdRepositories();

            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation(repositories);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new NivelClassificacaoCore(repositories, nivelClassificacaoValidation, mapper, organizacaoCore);

            NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel { Descricao = "Teste", Organizacao = new OrganizacaoModel { GuidOrganizacao = new Guid(_guidGees) } };

            _nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);
        }

        [TestMethod]
        public async Task NivelClassificacaoTestUpdateNull()
        {
            bool ok = false;

            try
            {
                await _core.UpdateAsync(null);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "O Nivel de Classificação não pode ser nulo.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com objeto nulo.");
        }

        #region Descrição
        [TestMethod]
        public async Task NivelClassificacaoTestUpdateWithDescricaoNull()
        {
            bool ok = false;
            try
            {
                _nivelClassificacaoModel.Descricao = null;

                await _core.UpdateAsync(_nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição nula.");
        }

        [TestMethod]
        public async Task NivelClassificacaoTestUpdateWithDescricaoEmpty()
        {
            bool ok = false;

            try
            {
                _nivelClassificacaoModel.Descricao = "";

                await _core.UpdateAsync(_nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição vazia.");
        }

        [TestMethod]
        public async Task NivelClassificacaoTestUpdateWithDescricaoTrimEmpty()
        {
            bool ok = false;

            try
            {
                _nivelClassificacaoModel.Descricao = " ";

                await _core.UpdateAsync(_nivelClassificacaoModel);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A descrição não pode ser vazia ou nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com descrição somente com espaço.");
        }
        #endregion

        #region Organização
        [TestMethod]
        public async Task NivelClassificacaoTestUpdateWithOrganizacaoNull()
        {
            bool ok = false;
            try
            {
                _nivelClassificacaoModel.Organizacao = null;

                await _core.UpdateAsync(_nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "A organização não pode ser nula.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com a organização nulo.");
        }

        [TestMethod]
        public async Task NivelClassificacaoTestUpdateWithGuidOrganizacaoGuidEmpty()
        {
            bool ok = false;

            try
            {
                _nivelClassificacaoModel.Organizacao.GuidOrganizacao = Guid.Empty;

                await _core.UpdateAsync(_nivelClassificacaoModel);

                ok = true;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ScdException));

                Assert.AreEqual(ex.Message, "Guid da organização inválido.");
            }

            if (ok)
                Assert.Fail("Não deveria ter atualizado com guid da organização sendo um guid vazio.");
        }

        //[TestMethod]
        //public async Task TestUpdateWithGuidOrganizacaoNonexistentOnOrganograma()
        //{
        //    bool ok = false;

        //    try
        //    {
        //        _nivelClassificacaoModel.GuidOrganizacao = Guid.NewGuid().ToString();

        //        await _core.UpdateAsync(_nivelClassificacaoModel);

        //        ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex, typeof(Exception));

        //        Assert.IsTrue(ex.Message.Contains("Não foi possível obter os dados do serviço."));
        //    }

        //    if (ok)
        //        Assert.Fail("Não deveria ter atualizado com o guid da organização não existindo no sistema de organograma.");
        //}
        #endregion

        //[TestMethod]
        //public async Task TestUpdateWithBasicsFields()
        //{
        //    NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
        //    {
        //        Codigo = "01",
        //        Descricao = "Descrição Teste",
        //        AreaFim = false,
        //        GuidOrganizacao = _guidGees
        //    };

        //    nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

        //    int id = nivelClassificacaoModel.Id;
        //    string codigo = "TestUpdateWithBasicsFields01";
        //    string descricao = "TestUpdateWithBasicsFieldsDescrição Teste";
        //    bool areaFim = true;
        //    string guidOrganizacao = _guidSeger;

        //    nivelClassificacaoModel.Codigo = codigo;
        //    nivelClassificacaoModel.Descricao = descricao;
        //    nivelClassificacaoModel.AreaFim = areaFim;
        //    nivelClassificacaoModel.GuidOrganizacao = guidOrganizacao;

        //    await _core.UpdateAsync(nivelClassificacaoModel);

        //    nivelClassificacaoModel = _core.Search(nivelClassificacaoModel.Id);

        //    Assert.IsTrue(nivelClassificacaoModel.Id == id);
        //    Assert.AreEqual(nivelClassificacaoModel.Codigo, codigo);
        //    Assert.AreEqual(nivelClassificacaoModel.Descricao, descricao);
        //    Assert.AreEqual(nivelClassificacaoModel.AreaFim, areaFim);
        //    Assert.AreEqual(nivelClassificacaoModel.GuidOrganizacao, guidOrganizacao);
        //    Assert.IsFalse(nivelClassificacaoModel.Aprovacao.HasValue);
        //    Assert.IsFalse(nivelClassificacaoModel.Publicacao.HasValue);
        //    Assert.IsFalse(nivelClassificacaoModel.InicioVigencia.HasValue);
        //    Assert.IsFalse(nivelClassificacaoModel.FimVigencia.HasValue);
        //}

        //[TestMethod]
        //public async Task TestUpdateWithCompleteFields()
        //{
        //    NivelClassificacaoModel nivelClassificacaoModel = new NivelClassificacaoModel
        //    {
        //        Codigo = "01",
        //        Descricao = "Descrição Teste",
        //        AreaFim = false,
        //        GuidOrganizacao = _guidGees
        //    };

        //    nivelClassificacaoModel = await _core.InsertAsync(nivelClassificacaoModel);

        //    DateTime now = DateTime.Now;

        //    int id = nivelClassificacaoModel.Id;
        //    string codigo = "TestUpdateWithCompleteFields01";
        //    string descricao = "TestUpdateWithCompleteFieldsDescrição Teste";
        //    bool areaFim = true;
        //    string guidOrganizacao = _guidSeger;
        //    DateTime aprovacao = now;
        //    DateTime publicacao = now.AddDays(1);
        //    DateTime inicioVigencia = now.AddDays(2);
        //    DateTime fimVigencia = now.AddDays(3);

        //    nivelClassificacaoModel.Codigo = codigo;
        //    nivelClassificacaoModel.Descricao = descricao;
        //    nivelClassificacaoModel.AreaFim = areaFim;
        //    nivelClassificacaoModel.GuidOrganizacao = guidOrganizacao;
        //    nivelClassificacaoModel.Aprovacao = aprovacao;
        //    nivelClassificacaoModel.Publicacao = publicacao;
        //    nivelClassificacaoModel.InicioVigencia = inicioVigencia;
        //    nivelClassificacaoModel.FimVigencia = fimVigencia;

        //    await _core.UpdateAsync(nivelClassificacaoModel);

        //    nivelClassificacaoModel = _core.Search(nivelClassificacaoModel.Id);

        //    Assert.IsTrue(nivelClassificacaoModel.Id == id);
        //    Assert.AreEqual(nivelClassificacaoModel.Codigo, codigo);
        //    Assert.AreEqual(nivelClassificacaoModel.Descricao, descricao);
        //    Assert.AreEqual(nivelClassificacaoModel.AreaFim, areaFim);
        //    Assert.AreEqual(nivelClassificacaoModel.GuidOrganizacao, guidOrganizacao);
        //    Assert.AreEqual(nivelClassificacaoModel.Aprovacao, aprovacao);
        //    Assert.AreEqual(nivelClassificacaoModel.Publicacao, publicacao);
        //    Assert.AreEqual(nivelClassificacaoModel.InicioVigencia, inicioVigencia);
        //    Assert.AreEqual(nivelClassificacaoModel.FimVigencia, fimVigencia);
        //}
    }
}