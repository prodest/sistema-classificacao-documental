using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.UnitTestBusiness.Common;

namespace Prodest.Scd.UnitTestBusiness.CriterioRestricao
{
    public class UnitTestCriterioRestricaoCommon : UnitTestBase
    {
        protected CriterioRestricaoCore _core;

        [TestInitialize]
        public void SetupSpecific()
        {
            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();
            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation);

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation(_repositories);

            CriterioRestricaoValidation criterioRestricaoValidation = new CriterioRestricaoValidation(_repositories, planoClassificacaoValidation);
            _core = new CriterioRestricaoCore(_repositories, criterioRestricaoValidation);
        }
    }
}