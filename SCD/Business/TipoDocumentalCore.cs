using AutoMapper;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class TipoDocumentalCore : ITipoDocumentalCore
    {
        private TipoDocumentalValidation _validation;
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<TipoDocumental> _tiposDocumentais;
        private IOrganizacaoCore _organizacaoCore;
        private IMapper _mapper;


        public TipoDocumentalCore(TipoDocumentalValidation validation, IScdRepositories repositories, IOrganizacaoCore organizacaoCore, IMapper mapper)
        {
            _validation = validation;
            _unitOfWork = repositories.UnitOfWork;
            _tiposDocumentais = repositories.TiposDocumentais;
            _organizacaoCore = organizacaoCore;
            _mapper = mapper;
        }

        public int Count(Guid guidOrganizacao)
        {
            //_validation.OrganizacaoValid(guidOrganizacao);

            //var count = _tiposDocumentais.Where(pc => pc.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
            //                                .Count();

            //return count;
            return 10;
        }

        public async Task DeleteAsync(int id)
        {
            //TipoDocumental tipoDocumental = SearchPersistence(id);

            //_validation.CanDelete(tipoDocumental);

            //_tiposDocumentais.Remove(tipoDocumental);

            //await _unitOfWork.SaveAsync();
        }

        public async Task<TipoDocumentalModel> InsertAsync(TipoDocumentalModel tipoDocumentalModel)
        {
            _validation.BasicValid(tipoDocumentalModel);

            _validation.IdInsertValid(tipoDocumentalModel.Id);

            ////TODO: Retirar este trecho quando o sistema conseguir obter organzação do usuário
            Guid guidProdest = new Guid(Environment.GetEnvironmentVariable("guidGEES"));
            OrganizacaoModel organizacao = _organizacaoCore.SearchAsync(guidProdest);

            tipoDocumentalModel.Organizacao = organizacao;
            tipoDocumentalModel.Ativo = true;

            TipoDocumental tipoDocumental = _mapper.Map<TipoDocumental>(tipoDocumentalModel);

            await _tiposDocumentais.AddAsync(tipoDocumental);

            await _unitOfWork.SaveAsync();

            tipoDocumentalModel = _mapper.Map<TipoDocumentalModel>(tipoDocumental);

            return tipoDocumentalModel;
        }

        public TipoDocumentalModel Search(int id)
        {
            //TipoDocumental tipoDocumental = SearchPersistence(id);

            //TipoDocumentalModel tipoDocumentalModel = _mapper.Map<TipoDocumentalModel>(tipoDocumental);

            //return tipoDocumentalModel;
            return new TipoDocumentalModel { Id = 1, Descricao = "Tipo Documental", Ativo = true, Organizacao = new OrganizacaoModel { Id = 1, GuidOrganizacao = new Guid(Environment.GetEnvironmentVariable("guidGEES")) } };
        }

        public List<TipoDocumentalModel> Search(Guid guidOrganizacao, int page, int count)
        {
            //            _validation.OrganizacaoValid(guidOrganizacao);

            //            _validation.PaginationSearch(page, count);

            //            int skip = (page - 1) * count;

            //            List<TipoDocumental> nivelsClassificacao = _tiposDocumentais.Where(pc => pc.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
            //                                                                               .OrderBy(pc => pc.Ativo)
            //                                                                               .ThenBy(pc => pc.Descricao)
            //                                                                               .Skip(skip)
            //                                                                               .Take(count)
            //                                                                               .ToList()
            //;
            //            List<TipoDocumentalModel> nivelsClassificacaoModel = _mapper.Map<List<TipoDocumentalModel>>(nivelsClassificacao);

            //            return nivelsClassificacaoModel;
            return new List<TipoDocumentalModel> { new TipoDocumentalModel { Id = 1, Descricao = "Tipo Documental", Ativo = true, Organizacao = new OrganizacaoModel { Id = 1, GuidOrganizacao = new Guid(Environment.GetEnvironmentVariable("guidGEES")) } } };
        }

        public async Task UpdateAsync(TipoDocumentalModel tipoDocumentalModel)
        {
            //_validation.Valid(tipoDocumentalModel);

            //TipoDocumental tipoDocumental = SearchPersistence(tipoDocumentalModel.Id);

            //_validation.CanUpdate(tipoDocumentalModel, tipoDocumental);

            //_mapper.Map(tipoDocumentalModel, tipoDocumental);

            //await _unitOfWork.SaveAsync();
        }

        private TipoDocumental SearchPersistence(int id)
        {
            _validation.IdValid(id);

            TipoDocumental tipoDocumental = _tiposDocumentais.Where(pc => pc.Id == id)
                                                             .SingleOrDefault();

            _validation.Found(tipoDocumental);

            return tipoDocumental;
        }
    }
}