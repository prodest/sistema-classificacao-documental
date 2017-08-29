using Prodest.Scd.Integration.Common;
using Prodest.Scd.Integration.Common.Base;
using Prodest.Scd.Integration.Organograma.Model;
using System.Threading.Tasks;

namespace Prodest.Scd.Integration.Organograma
{
    public class OrganogramaService
    {
        private IClientAccessTokenProvider _clientAccessToken;

        public OrganogramaService(IClientAccessTokenProvider clientAccessToken)
        {
            _clientAccessToken = clientAccessToken;
        }

        public async Task<OrganogramaOrganizacao> SearchAsync(string guid)
        {
            string url = $"https://sistemas.es.gov.br/prodest/organograma/api/organizacoes/{guid}";

            OrganogramaOrganizacao organizacao = await JsonData.DownloadAsync<OrganogramaOrganizacao>(url, _clientAccessToken.AccessToken);

            return organizacao;
        }

        public async Task<OrganogramaOrganizacao> SearchPatriarcaAsync(string guid)
        {
            string url = $"https://sistemas.es.gov.br/prodest/organograma/api/organizacoes/{guid}/patriarca";

            OrganogramaOrganizacao organizacao = await JsonData.DownloadAsync<OrganogramaOrganizacao>(url, _clientAccessToken.AccessToken);

            return organizacao;
        }
    }
}
