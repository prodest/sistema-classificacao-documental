using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prodest.Scd.Business;
using Prodest.Scd.Business.Validation;
using Prodest.Scd.Infrastructure.Integration;
using Prodest.Scd.UnitTestBusiness.Common;
using Prodest.Scd.Web.Configuration;

namespace Prodest.Scd.UnitTestBusiness.TermoClassificacaoInformacao
{
    public class UnitTestTermoClassificacaoInformacaoCommon : UnitTestBase
    {
        protected TermoClassificacaoInformacaoCore _core;

        [TestInitialize]
        public void SetupSpecific()
        {
            IOptions<AcessoCidadaoConfiguration> autenticacaoIdentityServerConfig = Options.Create(new AcessoCidadaoConfiguration { Authority = "https://acessocidadao.es.gov.br/is/" });
            AcessoCidadaoClientAccessToken acessoCidadaoClientAccessToken = new AcessoCidadaoClientAccessToken(autenticacaoIdentityServerConfig);
            OrganogramaService organogramaService = new OrganogramaService(acessoCidadaoClientAccessToken);

            OrganizacaoValidation organizacaoValidation = new OrganizacaoValidation();
            OrganizacaoCore organizacaoCore = new OrganizacaoCore(_repositories, organizacaoValidation);

            PlanoClassificacaoValidation planoClassificacaoValidation = new PlanoClassificacaoValidation(_repositories);
            PlanoClassificacaoCore planoClassificacaoCore = new PlanoClassificacaoCore(_repositories, organogramaService, organizacaoCore);

            NivelClassificacaoValidation nivelClassificacaoValidation = new NivelClassificacaoValidation();
            NivelClassificacaoCore nivelClassificacaoCore = new NivelClassificacaoCore(_repositories, nivelClassificacaoValidation, organizacaoCore);

            ItemPlanoClassificacaoValidation itemPlanoClassificacaoValidation = new ItemPlanoClassificacaoValidation(nivelClassificacaoCore, planoClassificacaoCore, planoClassificacaoValidation);
            ItemPlanoClassificacaoCore itemPlanoClassificacaoCore = new ItemPlanoClassificacaoCore(_repositories, itemPlanoClassificacaoValidation);

            TipoDocumentalValidation tipoDocumentalValidation = new TipoDocumentalValidation();
            TipoDocumentalCore tipoDocumentalCore = new TipoDocumentalCore(tipoDocumentalValidation, _repositories, organizacaoCore);

            TermoClassificacaoInformacaoValidation termoClassificacaoInformacaoValidation = new TermoClassificacaoInformacaoValidation(_repositories, itemPlanoClassificacaoCore, tipoDocumentalCore, planoClassificacaoValidation);
            _core = new TermoClassificacaoInformacaoCore(_repositories, termoClassificacaoInformacaoValidation);
        }
    }
}