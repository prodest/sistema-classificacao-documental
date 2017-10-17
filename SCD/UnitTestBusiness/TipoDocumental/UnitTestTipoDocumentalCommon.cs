using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
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
    public class UnitTestTipoDocumentalCommon
    {
        private IMapper _mapper;
        protected ScdRepositories _repositories;
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

        protected async Task<TipoDocumentalModel> InsertModelAsync()
        {
            Persistence.Model.TipoDocumental tipoDocumental = await InsertAsync();

            TipoDocumentalModel tipoDocumentalModel = _mapper.Map<TipoDocumentalModel>(tipoDocumental);

            return tipoDocumentalModel;
        }

        protected Persistence.Model.TipoDocumental SearchAsync(int id)
        {
            Persistence.Model.TipoDocumental tipoDocumental = _repositories.TiposDocumentais.Where(td => td.Id == id)
                                                                                            .SingleOrDefault();

            return tipoDocumental;
        }

        protected TipoDocumentalModel SearchModelAsync(int id)
        {
            Persistence.Model.TipoDocumental tipoDocumental = SearchAsync(id);

            TipoDocumentalModel tipoDocumentalModel = _mapper.Map<TipoDocumentalModel>(tipoDocumental);

            return tipoDocumentalModel;
        }

        [TestInitialize]
        public void Setup()
        {
            TipoDocumentalValidation tipoDocumentalValidation = new TipoDocumentalValidation();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            _mapper = Mapper.Instance;

            _repositories = new ScdRepositories(_mapper);

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation, _mapper);

            _core = new TipoDocumentalCore(tipoDocumentalValidation, _repositories, organizacaoCore, _mapper);
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
