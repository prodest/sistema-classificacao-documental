using Prodest.Scd.Infrastructure.Integration.Common;
using Prodest.Scd.Integration.Common.Base;
using Prodest.Scd.Integration.Organograma.Base;
using Prodest.Scd.Integration.Organograma.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prodest.Scd.Infrastructure.Integration
{
    public class OrganogramaService : IOrganogramaService
    {
        private IClientAccessTokenProvider _clientAccessToken;

        public OrganogramaService(IClientAccessTokenProvider clientAccessToken)
        {
            _clientAccessToken = clientAccessToken;
        }

        public async Task<List<OrganogramaOrganizacao>> SearchAsync()
        {
            string url = $"https://sistemas.es.gov.br/prodest/organograma/api/organizacoes/{Environment.GetEnvironmentVariable("GuidGEES")}/filhas";

            List<OrganogramaOrganizacao> organizacoes = await JsonData.DownloadAsync<List<OrganogramaOrganizacao>>(url, _clientAccessToken.AccessToken);

            return organizacoes;
        }

        public async Task<OrganogramaOrganizacao> SearchAsync(Guid guidOrganizacao)
        {
            string url = $"https://sistemas.es.gov.br/prodest/organograma/api/organizacoes/{guidOrganizacao.ToString()}";

            OrganogramaOrganizacao organizacao = await JsonData.DownloadAsync<OrganogramaOrganizacao>(url, _clientAccessToken.AccessToken);

            return organizacao;
        }

        public async Task<OrganogramaOrganizacao> SearchPatriarcaAsync(Guid guidOrganizacao)
        {
            string url = $"https://sistemas.es.gov.br/prodest/organograma/api/organizacoes/{guidOrganizacao.ToString()}/patriarca";

            OrganogramaOrganizacao organizacao = await JsonData.DownloadAsync<OrganogramaOrganizacao>(url, _clientAccessToken.AccessToken);

            return organizacao;
        }
    }
}
