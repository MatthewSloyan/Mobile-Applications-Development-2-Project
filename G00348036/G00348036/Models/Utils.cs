using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using unirest_net.http;
using unirest_net.request;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        public static void AddToFavourites(SearchByIngredientsData selectedRecipe)
        {
            // Create a new list of FavouriteRecipesData
            List<FavouriteRecipesData> list = new List<FavouriteRecipesData>();
            string fileString;
            string path;
            string fileName;

            //fill the list, and read a local folder
            try
            {
                //read the local file 
                path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                fileName = Path.Combine(path, FAVOURITES_SAVE_FILE);

                // If the file doesn't exist create the file
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Dispose();
                }
                else
                {
                    // Otherwise read in file to list
                    using (var reader = new StreamReader(fileName))
                    {
                        fileString = reader.ReadToEnd();
                        list = JsonConvert.DeserializeObject<List<FavouriteRecipesData>>(fileString);
                    }
                }

                // Create new object and populate with passed in data, and add to list
                FavouriteRecipesData fav = new FavouriteRecipesData
                {
                    id = selectedRecipe.id,
                    title = selectedRecipe.title,
                    image = selectedRecipe.image
                };
                list.Add(fav);

                // Write updated list back out to file
                using (var writer = new StreamWriter(fileName, false))
                {
                    string stringifiedText = JsonConvert.SerializeObject(list);
                    writer.WriteLine(stringifiedText);
                }
            }
            catch
            {
                //await DisplayAlert("Error", "There are no favourites saved, please add some and return", "OK");
            }
        }
    }
}
