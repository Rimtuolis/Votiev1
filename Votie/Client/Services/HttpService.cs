namespace Votie.Client.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }

        public void SetHeader(string accessJWT)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessJWT);
        }
        public void ClearHeader()
        { 
            _httpClient.DefaultRequestHeaders.Clear();
        }
    }
}
