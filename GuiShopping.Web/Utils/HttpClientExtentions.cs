using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GuiShopping.Web.Utils
{
    public static class HttpClientExtentions
    {
        private static  MediaTypeHeaderValue contentType
            = new MediaTypeHeaderValue("application/json");
        public static async Task <T> ReadContentAs<T> (this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Algo deu errado: " +
                $"{response.ReasonPhrase}");
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(dataAsString,
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true,
               });
        }
        public static Task <HttpResponseMessage> PostAsJson<T>(
            this HttpClient httpClient , string url , T data)
        {
            var dataasString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataasString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsync(url, content);
        }
        public static Task <HttpResponseMessage> PUtAsJson<T>(
            this HttpClient httpClient , string url , T data)
        {
            var dataasString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataasString);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsync(url, content);
        }
    }
}
