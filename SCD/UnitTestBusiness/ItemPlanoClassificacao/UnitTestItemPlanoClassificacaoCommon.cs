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
            ItemPlanoClassificacaoValidation itemPlanoClassificacaoValidation = new ItemPlanoClassificacaoValidation();

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();

            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation);

            _core = new ItemPlanoClassificacaoCore(_repositories, itemPlanoClassificacaoValidation, organizacaoCore);
        }
    }
}