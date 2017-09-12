using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Configuration;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prodest.Scd.UnitTestBusiness.ItemPlanoClassificacao
{
    [TestClass]
    public class UnitTestItemPlanoClassificacaoInsert
    {
        private string _guidGees = Environment.GetEnvironmentVariable("GuidGEES");
        private ItemPlanoClassificacaoCore _core;

        [TestInitialize]
        public void Setup()
        {
            ScdRepositories repositories = new ScdRepositories();

            ItemPlanoClassificacaoValidation itemPlanoClassificacaoValidation = new ItemPlanoClassificacaoValidation(repositories);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BusinessProfileAutoMapper>();
            });

            IMapper mapper = Mapper.Instance;

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(repositories, organizacaoValidation, mapper);

            //_core = new ItemPlanoClassificacaoCore(repositories, itemPlanoClassificacaoValidation, mapper, organizacaoCore);
        }

    }
}
