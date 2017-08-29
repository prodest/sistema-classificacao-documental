﻿using Newtonsoft.Json;
using Prodest.Scd.Infrastructure.Common.Exceptions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Prodest.Scd.Integration.Common
{
    public class JsonData
    {
        public static async Task<T> DownloadAsync<T>(string url, string accessToken) where T : new()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
                var result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
                }
                else
                {
                    string mensagemErro = result.StatusCode + ": " + result.Content;
                    throw new ScdExpection("Não foi possível obter os dados do serviço." + mensagemErro);
                }
            }
        }
    }
}
