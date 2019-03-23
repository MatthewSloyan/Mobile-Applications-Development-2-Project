using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using unirest_net.http;
using unirest_net.request;

namespace G00348036
{
    class Utils
    {
        public static List<T> GetApiData<T>(string URL)
        {
            HttpRequest request = Unirest.get(URL)
                .header("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
            HttpResponse<string> response = request.asString();

            List<T> results = JsonConvert.DeserializeObject<List<T>>(response.Body);
            return results;
        }

        public static T GetSingleApiData<T>(string URL)
        {
            HttpRequest request = Unirest.get(URL)
                .header("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
            HttpResponse<string> response = request.asString();

            T result = JsonConvert.DeserializeObject<T>(response.Body);
            return result;
        }

        //public static async System.Threading.Tasks.Task<List<T>> GetApiDataAsync<T>(string URL)
        //{
        //    List<T> results = null;
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
        //    HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        results = JsonConvert.DeserializeObject<List<T>>(content);
        //    }

        //    //HttpRequest request = Unirest.get(URL)
        //    //    .header("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
        //    //HttpResponse<string> response = request.asString();


        //    return results;
        //}
    }
}
