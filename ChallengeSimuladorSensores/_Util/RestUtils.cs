using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChallengeSimuladorSensores._Util
{
    public  class RestUtils
    {

        private HttpClient HttpClientFactory { get; }

        public RestUtils(HttpClient httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public async Task<string> Delete(string request, string content)
        {
            try
            {
                var client = HttpClientFactory; //HttpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Accept.Clear();

                var resp = client.SendAsync(
                    new HttpRequestMessage(HttpMethod.Delete, request)
                    {
                        Content = new StringContent(content, Encoding.UTF8, "application/json")
                    }
                    ).Result;
                if (resp.IsSuccessStatusCode)
                    return await resp.Content.ReadAsStringAsync();
                else
                    throw new HttpRequestException(resp.ReasonPhrase, new ApplicationException(resp.Content.ReadAsStringAsync().Result), resp.StatusCode);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> Post(string request, string content)
        {
            try
            {
                var client = HttpClientFactory;// HttpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Accept.Clear();

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var resp = client.PostAsync(request, byteContent).Result;
                if (resp.IsSuccessStatusCode)
                    return await resp.Content.ReadAsStringAsync();
                else
                    throw new HttpRequestException(resp.ReasonPhrase, new ApplicationException(resp.Content.ReadAsStringAsync().Result), resp.StatusCode);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> Put(string request, string content)
        {
            try
            {
                var client = HttpClientFactory; //HttpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Accept.Clear();

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var resp = client.PutAsync(request, byteContent).Result;
                if (resp.IsSuccessStatusCode)
                    return await resp.Content.ReadAsStringAsync();
                else
                    throw new HttpRequestException(resp.ReasonPhrase, new ApplicationException(resp.Content.ReadAsStringAsync().Result), resp.StatusCode);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //TODO Tratamento de mensagens de erro
        public async Task<string> GetString(string request)
        {
            try
            {
                var client = HttpClientFactory; //HttpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Accept.Clear();
                var resp = client.GetAsync(request).Result;
                if (resp.IsSuccessStatusCode)
                    return await resp.Content.ReadAsStringAsync();
                else
                    throw new HttpRequestException(resp.ReasonPhrase, new ApplicationException(resp.Content.ReadAsStringAsync().Result), resp.StatusCode);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string QueryString(Object opcoes)
        {
            var sb = new StringBuilder();
            Type t = opcoes.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (var prop in props)
            {
                if (prop?.GetValue(opcoes) is ICollection)
                {
                    var lista = prop.GetValue(opcoes) as IEnumerable;
                    foreach (var item in lista)
                    {
                        sb.Append($"&{HttpUtility.UrlEncode(prop.Name)}={HttpUtility.UrlEncode(item.ToString(), Encoding.UTF8)}");

                    }

                }
                else if (prop.GetIndexParameters().Length == 0 && prop.GetValue(opcoes) != null)
                    sb.Append($"&{HttpUtility.UrlEncode(prop.Name)}={HttpUtility.UrlEncode(prop.GetValue(opcoes).ToString(), Encoding.UTF8)}");

            }
            if (sb.Length > 0)
                return "?" + sb.ToString()[1..];
            else
                return string.Empty;
        }


    }
}
