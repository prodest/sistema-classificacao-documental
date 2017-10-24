using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.UnitTestBusiness.Common;

namespace Prodest.Scd.UnitTestBusiness.ItemPlanoClassificacao
{
    public class UnitTestItemPlanoClassificacaoCommon : UnitTestBase
    {
        protected ItemPlanoClassificacaoCore _core;

        [TestInitialize]
        public void SetupSpecific()
        {
            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();
            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation);

            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation();
            NivelClassificacaoCore nivelClassificacaoCore = new NivelClassificacaoCore(_repositories, nivelClassificacaoValidation, organizacaoCore);

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation();
            PlanoClassificacaoCore planoClassificacaoCore = new PlanoClassificacaoCore(_repositories, _organogramaService, organizacaoCore);

            ItemPlanoClassificacaoValidation itemPlanoClassificacaoValidation = new ItemPlanoClassificacaoValidation(_repositories.ItensPlanoClassificacaoSpecific, nivelClassificacaoCore, planoClassificacaoCore, planoClassificacaoValidation);
            _core = new ItemPlanoClassificacaoCore(_repositories, itemPlanoClassificacaoValidation, organizacaoCore);
        }
    }
}