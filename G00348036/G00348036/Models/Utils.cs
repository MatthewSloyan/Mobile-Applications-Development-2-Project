using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using unirest_net.http;
using unirest_net.request;

namespace G00348036
{
    class Utils
    {
        private const string FAVOURITES_SAVE_FILE = "favourites.txt";

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

        //public static void AddToFavourites<T>(T selectedRecipe)
        //{
        //    ObservableCollection<T> list = new ObservableCollection<T>();
        //    string fileString;

        //    //fill the list, and read a local folder
        //    try
        //    {
        //        //read the local file
        //        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        //        string fileName = Path.Combine(path, FAVOURITES_SAVE_FILE);

        //        using (var reader = new StreamReader(fileName))
        //        {
        //            fileString = reader.ReadToEnd();
        //            list = JsonConvert.DeserializeObject<ObservableCollection<T>>(fileString);
        //        }
        //    }
        //    catch
        //    {
        //        DisplayAlert("Error", "There are no favourites saved, please add some and return", "OK");
        //    }

        //    //read the file
        //    string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        //    string fileName = Path.Combine(path, FAVOURITES_SAVE_FILE);

        //    using (var writer = new StreamWriter(fileName, false))
        //    {
        //        string stringifiedText = JsonConvert.SerializeObject(list);
        //        writer.WriteLine(stringifiedText);
        //    }
        //}
    }
}
