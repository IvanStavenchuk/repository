using Common.Objects;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Desktop.Models
{
    internal class BaseModel<TObject> where TObject : BaseObject
    {
        private HttpClient _client;
        private string _requestUri;

        internal BaseModel(string uriString, string requestUri)
        {
            _requestUri = requestUri;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(uriString);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal async Task<TObject> GetItemAsync(string path)
        {
            TObject Item = null;
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Item = await response.Content.ReadAsAsync<TObject>();
            }
            return Item;
        }
        internal async Task<TObject> CreateProductAsync(TObject Item)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(
                _requestUri, Item);
            response.EnsureSuccessStatusCode();

            return  await GetItemAsync(response.Headers.Location.PathAndQuery);
        }
        internal async Task<TObject> UpdateItemAsync(TObject Item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(
                $"{_requestUri}/{Item.Id}", Item);
            response.EnsureSuccessStatusCode();

            Item = await response.Content.ReadAsAsync<TObject>();
            return Item;
        }
        internal async Task<HttpStatusCode> DeleteItemAsync(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(
                $"{_requestUri}/{id}");
            return response.StatusCode;
        }
    }
}
