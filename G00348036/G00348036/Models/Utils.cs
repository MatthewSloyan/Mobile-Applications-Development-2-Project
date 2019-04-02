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
        // Global constant for file name
        public const string FAVOURITES_SAVE_FILE = "favourites.txt";

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

        // Generic method to read in list from file
        public static ObservableCollection<T> getListFromFile<T>()
        {
            ObservableCollection<T> list = new ObservableCollection<T>();
            try
            {
                ReadFromFile(ref list, FAVOURITES_SAVE_FILE);
            }
            catch
            {
                // Error alert
            }

            return list;
        }
        
        public static void AddToFavourites(SearchByIngredientsData selectedRecipe)
        {
            // Create a new list of FavouriteRecipesData
            ObservableCollection<FavouriteRecipesData> list = new ObservableCollection<FavouriteRecipesData>();

            //fill the list, and read a local folder
            try
            {
                // Reads in file and updates list above using ref, and returns the full file path
                string fullPath = ReadFromFile(ref list, FAVOURITES_SAVE_FILE);

                // Create new object and populate with passed in data, and add to list
                FavouriteRecipesData fav = new FavouriteRecipesData
                {
                    id = selectedRecipe.id,
                    title = selectedRecipe.title,
                    image = selectedRecipe.image
                };
                list.Add(fav);

                WriteListToFile(list, fullPath);
            }
            catch
            {
                //await DisplayAlert("Error", "There are no favourites saved, please add some and return", "OK");
            }
        }

        public static void AddToFavouritesR(SearchByRecipeData.Result selectedRecipe)
        {
            // Create a new list of FavouriteRecipesData
            ObservableCollection<FavouriteRecipesData> list = new ObservableCollection<FavouriteRecipesData>();

            //fill the list, and read a local folder
            try
            {
                // Reads in file and updates list above using ref, and returns the full file path
                string fullPath = ReadFromFile(ref list, FAVOURITES_SAVE_FILE);

                // Create new object and populate with passed in data, and add to list
                FavouriteRecipesData fav = new FavouriteRecipesData
                {
                    id = selectedRecipe.id,
                    title = selectedRecipe.title,
                    image = selectedRecipe.image
                };
                list.Add(fav);

                WriteListToFile(list, fullPath);
            }
            catch
            {
                //await DisplayAlert("Error", "There are no favourites saved, please add some and return", "OK");
            }
        }

        // Remove object from file
        public static void RemoveFavouriteFromFile(FavouriteRecipesData recipeObject)
        {
            // Create a new list of FavouriteRecipesData
            ObservableCollection<FavouriteRecipesData> list = new ObservableCollection<FavouriteRecipesData>();

            try
            {
                //read the local file 
                string fullPath = ReadFromFile(ref list, FAVOURITES_SAVE_FILE);

                int count = 0;
                foreach (var item in list)
                {
                    if (item.id == recipeObject.id)
                    {
                        list.RemoveAt(count);
                        break;
                    }
                    count++;
                }

                WriteListToFile(list, fullPath);
            }
            catch
            {
                //await DisplayAlert("Error", "There are no favourites saved, please add some and return", "OK");
            }
        }

        // Generic read from file method
        private static string ReadFromFile<T>(ref ObservableCollection<T> list, string fileName)
        {
            string fileString;
            string path;
            string fullPath;

            //read the local file 
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            fullPath = Path.Combine(path, fileName);

            //fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);

            // If the file doesn't exist create the file
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath).Dispose();
            }
            else
            {
                // Otherwise read in file to list
                using (var reader = new StreamReader(fullPath))
                {
                    fileString = reader.ReadToEnd();
                    list = JsonConvert.DeserializeObject<ObservableCollection<T>>(fileString);
                }
            }
            return fullPath;
        }

        // Generic write to file method
        private static void WriteListToFile(ObservableCollection<FavouriteRecipesData> list, string fullPath)
        {
            // Write updated list back out to file
            using (var writer = new StreamWriter(fullPath, false))
            {
                string stringifiedText = JsonConvert.SerializeObject(list);
                writer.WriteLine(stringifiedText);
            }
        }
    }
}
