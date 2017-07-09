using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankManager_Csharp_Console
{
    public static class HttpUtility
    {
        public static async Task<String> Post(HttpClient client, String url, FormUrlEncodedContent data)
        {
            HttpResponseMessage response = await client.PostAsync(url, data);


            var contents = await response.Content.ReadAsStringAsync();

            return contents;
        }

        public static async Task<String> Get(HttpClient client, String url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            var contents = await response.Content.ReadAsStringAsync();
            return contents;
        }
    }
}
