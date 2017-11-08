using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.UnitTestBusiness.Common;

namespace Prodest.Scd.UnitTestBusiness.Temporalidade
{
    public class UnitTestTemporalidadeCommon : UnitTestBase
    {
        protected TemporalidadeCore _core;

        [TestInitialize]
        public void SetupSpecific()
        {
            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();
            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation);

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation(_repositories);

            TemporalidadeValidation temporalidadeValidation = new TemporalidadeValidation(_repositories, planoClassificacaoValidation);
            _core = new TemporalidadeCore(_repositories, temporalidadeValidation);
        }
    }
}