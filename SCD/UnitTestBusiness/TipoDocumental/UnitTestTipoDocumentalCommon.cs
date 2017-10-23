using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Configuration;
using Prodest.Scd.Infrastructure.Repository.Specific;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.UnitTestBusiness.TipoDocumental
{
    public class UnitTestTipoDocumentalCommon
    {
        private IMapper _mapper;
        protected EFScdRepositories _repositories;
        protected List<int> _idsTiposDocumentaisTestados = new List<int>();
        protected TipoDocumentalCore _core;
        protected Guid _guidGees = new Guid(Environment.GetEnvironmentVariable("GuidGEES"));

        protected async Task<TipoDocumentalModel> InsertAsync()
        {
            TipoDocumentalModel tipoDocumentalModel = new TipoDocumentalModel { Descricao = "Tipo Documental Teste", Ativo = true, Organizacao = new OrganizacaoModel { Id = 1 } };

            tipoDocumentalModel = await _repositories.TiposDocumentaisSpecific.AddAsync(tipoDocumentalModel);

            _idsTiposDocumentaisTestados.Add(tipoDocumentalModel.Id);

            return tipoDocumentalModel;
        }

        protected async Task<TipoDocumentalModel> InsertModelAsync()
        {
            TipoDocumentalModel tipoDocumentalModel = await InsertAsync();

            return tipoDocumentalModel;
        }

        protected async Task<TipoDocumentalModel> SearchAsync(int id)
        {
            TipoDocumentalModel tipoDocumentalModel = await _repositories.TiposDocumentaisSpecific.SearchAsync(id);

            return tipoDocumentalModel;
        }

        protected async Task<TipoDocumentalModel> SearchModelAsync(int id)
        {
            TipoDocumentalModel tipoDocumentalModel = await SearchAsync(id);

            return tipoDocumentalModel;
        }

        [TestInitialize]
        public void Setup()
        {
            TipoDocumentalValidation tipoDocumentalValidation = new TipoDocumentalValidation();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<InfrastructureProfileAutoMapper>();
            });

            _mapper = Mapper.Instance;

            _repositories = new EFScdRepositories(_mapper);

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation);

            _core = new TipoDocumentalCore(tipoDocumentalValidation, _repositories, organizacaoCore);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (int idTipoDocumental in _idsTiposDocumentaisTestados)
            {
                TipoDocumentalModel tipoDocumentalModel = await _repositories.TiposDocumentaisSpecific.SearchAsync(idTipoDocumental);

                if (tipoDocumentalModel != null)
                    await _repositories.TiposDocumentaisSpecific.RemoveAsync(tipoDocumentalModel.Id);
            }

            await _repositories.UnitOfWork.SaveAsync();
        }

    }
}
