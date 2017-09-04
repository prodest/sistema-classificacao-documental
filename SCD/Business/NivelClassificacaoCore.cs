using AutoMapper;
using Prodest.Scd.Business.Base;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Integration.Organograma.Base;
using Prodest.Scd.Integration.Organograma.Model;
using Prodest.Scd.Persistence.Base;
using Prodest.Scd.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Business
{
    public class NivelClassificacaoCore : INivelClassificacaoCore
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<NivelClassificacao> _nivelsClassificacao;
        private NivelClassificacaoValidation _validation;
        private IMapper _mapper;
        private IOrganogramaService _organogramaService;
        private IOrganizacaoCore _organizacaoCore;

        public NivelClassificacaoCore(IScdRepositories repositories, NivelClassificacaoValidation validation, IMapper mapper, IOrganogramaService organogramaService, IOrganizacaoCore organizacaoCore)
        {
            _unitOfWork = repositories.UnitOfWork;
            _nivelsClassificacao = repositories.NivelsClassificacao;
            _validation = validation;
            _mapper = mapper;
            _organogramaService = organogramaService;
            _organizacaoCore = organizacaoCore;
        }

        public int Count(string guidOrganizacao)
        {
            _validation.OrganizacaoFilled(guidOrganizacao);
            _validation.OrganizacaoValid(guidOrganizacao);

            Guid guid = new Guid(guidOrganizacao);

            var count = _nivelsClassificacao.Where(pc => pc.GuidOrganizacao.Equals(guid))
                                                  .Count();

            return count;
        }

        public async Task DeleteAsync(int id)
        {
            NivelClassificacao nivelClassificacao = SearchPersistence(id);

            _validation.CanDelete(nivelClassificacao);

            _nivelsClassificacao.Remove(nivelClassificacao);

            await _unitOfWork.SaveAsync();
        }

        public async Task<NivelClassificacaoModel> InsertAsync(NivelClassificacaoModel nivelClassificacaoModel)
        {
            _validation.BasicValid(nivelClassificacaoModel);

            _validation.IdInsertValid(nivelClassificacaoModel.Id);

            OrganogramaOrganizacao organogramaOrganizacaoPatriarca = await _organogramaService.SearchPatriarcaAsync(nivelClassificacaoModel.GuidOrganizacao);

            OrganizacaoModel organizacaoPatriarca = _organizacaoCore.SearchAsync(organogramaOrganizacaoPatriarca.Guid.ToString());

            nivelClassificacaoModel.Organizacao = organizacaoPatriarca;

            NivelClassificacao nivelClassificacao = _mapper.Map<NivelClassificacao>(nivelClassificacaoModel);

            await _nivelsClassificacao.AddAsync(nivelClassificacao);

            await _unitOfWork.SaveAsync();

            nivelClassificacaoModel = _mapper.Map<NivelClassificacaoModel>(nivelClassificacao);

            return nivelClassificacaoModel;
        }

        public NivelClassificacaoModel Search(int id)
        {
            NivelClassificacao nivelClassificacao = SearchPersistence(id);

            NivelClassificacaoModel nivelClassificacaoModel = _mapper.Map<NivelClassificacaoModel>(nivelClassificacao);

            return nivelClassificacaoModel;
        }

        public List<NivelClassificacaoModel> Search(string guidOrganizacao, int page, int count)
        {
            _validation.OrganizacaoFilled(guidOrganizacao);
            _validation.OrganizacaoValid(guidOrganizacao);

            _validation.PaginationSearch(page, count);

            Guid guid = new Guid(guidOrganizacao);
            int skip = (page - 1) * count;

            List<NivelClassificacao> nivelsClassificacao = _nivelsClassificacao.Where(pc => pc.GuidOrganizacao.Equals(guid))
                                                                               .OrderBy(pc => pc.InicioVigencia.HasValue)
                                                                               .ThenByDescending(pc => pc.InicioVigencia)
                                                                               .ThenByDescending(pc => pc.Codigo)
                                                                               .Skip(skip)
                                                                               .Take(count)
                                                                               .ToList()
                                                                               ;
            List<NivelClassificacaoModel> nivelsClassificacaoModel = _mapper.Map<List<NivelClassificacaoModel>>(nivelsClassificacao);

            return nivelsClassificacaoModel;
        }

        public async Task UpdateAsync(NivelClassificacaoModel nivelClassificacaoModel)
        {
            _validation.Valid(nivelClassificacaoModel);

            NivelClassificacao nivelClassificacao = SearchPersistence(nivelClassificacaoModel.Id);

            _validation.CanUpdate(nivelClassificacao);

            if (!nivelClassificacaoModel.GuidOrganizacao.ToUpper().Equals(nivelClassificacao.GuidOrganizacao.ToString().ToUpper()))
            {
                OrganogramaOrganizacao organogramaOrganizacaoPatriarca = await _organogramaService.SearchPatriarcaAsync(nivelClassificacaoModel.GuidOrganizacao);

                OrganizacaoModel organizacaoPatriarca = _organizacaoCore.SearchAsync(organogramaOrganizacaoPatriarca.Guid.ToString());

                if (nivelClassificacao.IdOrganizacao != organizacaoPatriarca.Id)
                    nivelClassificacaoModel.Organizacao = organizacaoPatriarca;
            }

            _mapper.Map(nivelClassificacaoModel, nivelClassificacao);

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateFimVigenciaAsync(int id, DateTime fimVigencia)
        {
            NivelClassificacao nivelClassificacao = SearchPersistence(id);

            _validation.FimVigenciaValid(fimVigencia, nivelClassificacao.InicioVigencia);

            nivelClassificacao.FimVigencia = fimVigencia;

            await _unitOfWork.SaveAsync();
        }

        private NivelClassificacao SearchPersistence(int id)
        {
            _validation.IdValid(id);

            NivelClassificacao nivelClassificacao = _nivelsClassificacao.Where(pc => pc.Id == id)
                                                                              .SingleOrDefault();

            _validation.Found(nivelClassificacao);

            return nivelClassificacao;
        }
    }
}