using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.TipoDocumental
{
    public class UnitTestTipoDocumentalCommon
    {
        protected ScdRepositories _repositories = new ScdRepositories();
        protected List<int> _idsTiposDocumentaisTestados = new List<int>();
        protected TipoDocumentalCore _core;
        protected Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("GuidGEES"));

        protected async Task<Persistence.Model.TipoDocumental> InsertAsync()
        {
            Persistence.Model.TipoDocumental tipoDocumental = new Persistence.Model.TipoDocumental { Descricao = "Tipo Documental Teste", Ativo = true, IdOrganizacao = 1 };

            tipoDocumental = await _repositories.TiposDocumentais.AddAsync(tipoDocumental);

            await _repositories.UnitOfWork.SaveAsync();

            _idsTiposDocumentaisTestados.Add(tipoDocumental.Id);

            return tipoDocumental;
        }

        [TestInitialize]
        public void Setup()
        {
            TipoDocumentalValidation tipoDocumentalValidation = new TipoDocumentalValidation();

            ScdRepositories repositories = new ScdRepositories();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            _core = new TipoDocumentalCore(tipoDocumentalValidation, repositories, organizacaoCore, mapper);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (int idTipoDocumental in _idsTiposDocumentaisTestados)
            {
                Persistence.Model.TipoDocumental tipoDocumental = _repositories.TiposDocumentais.SingleOrDefault(td => td.Id == idTipoDocumental);

                if (tipoDocumental != null)
                    _repositories.TiposDocumentais.Remove(tipoDocumental);
            }

            await _repositories.UnitOfWork.SaveAsync();
        }

    }
}
