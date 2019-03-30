using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using unirest_net.http;
using unirest_net.request;

namespace G00348036
{
    class Utils
    {
        public static ObservableCollection<T> GetApiData<T>(string URL)
        {
            HttpRequest request = Unirest.get(URL)
                .header("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
            HttpResponse<string> response = request.asString();
            string test = URL;
            System.Diagnostics.Debug.WriteLine(response.Body);

            ObservableCollection<T> results = JsonConvert.DeserializeObject<ObservableCollection<T>>(response.Body);
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
    }
}
