using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using unirest_net.http;
using unirest_net.request;

namespace G00348036
{
    class Utils
    {
        // Global constant for file name
        public const string FAVOURITES_SAVE_FILE = "favourites.txt";

        /*
         * I have made all these methods with generics if possible
         * so that if more features/functionality was added in the future the methods 
         * would be resusable without changing them.
         * */

        // Generic method which gets and parses lists of objects from a url using The spoonacular Nutrition, Recipe, and Food API. 
        // Used to get lists in searchByIngredients and SearchByImage as json returned is a list of objects
        public static ObservableCollection<T> GetApiData<T>(string URL)
        {
            HttpRequest request = Unirest.get(URL)
                .header("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
            HttpResponse<string> response = request.asString();
            string test = URL;

            ObservableCollection<T> results = JsonConvert.DeserializeObject<ObservableCollection<T>>(response.Body);
            return results;
        }

        // Generic method which gets and parses a single object from a url using The spoonacular Nutrition, Recipe, and Food API
        // Used to get SearchByRecipe, random recipes and getting instructions as json returned is a object with a nested list
        public static T GetSingleApiData<T>(string URL)
        {
            HttpRequest request = Unirest.get(URL)
                .header("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
            HttpResponse<string> response = request.asString();

            T result = JsonConvert.DeserializeObject<T>(response.Body);
            return result;
        }

        // Generic method to read in ObservableCollection list from file
        // Used to read in list on Favourites page
        public static ObservableCollection<T> getListFromFile<T>()
        {
            // List is updated when method is called (ref)
            ObservableCollection<T> list = new ObservableCollection<T>();
            try
            {
                ReadFromFile(ref list, FAVOURITES_SAVE_FILE);
            }
            catch{}
            return list;
        }
        
        // Add to favourites method which adds object passed in to list from file and then writes to file again
        // Couldn't use generic type as needed to access variables from object passed in
        public static void AddToFavourites(RecipeResults selectedRecipe)
        {
            // Create a new list of FavouriteRecipesData, which is updated in ReadFromFile()
            ObservableCollection<RecipesData> list = new ObservableCollection<RecipesData>();

            //fill the list, and read a local folder
            try
            {
                // Reads in file and updates list above using ref, and returns the full file path
                string fullPath = ReadFromFile(ref list, FAVOURITES_SAVE_FILE);

                // If the list is null E.g. on first start up, then create new list which solves the bug which wouldn't
                // allow the user to add to favourite if they have no favourites saved.
                if(list == null)
                {
                    list = new ObservableCollection<RecipesData>();
                }

                // Create new object and populate with passed in data, and add to list
                RecipesData fav = new RecipesData
                {
                    id = selectedRecipe.id,
                    title = selectedRecipe.title,
                    image = selectedRecipe.image
                };
                list.Add(fav);

                // Write new list back out to file adding the new recipe
                WriteListToFile(list, fullPath);
            }
            catch{ }
        }
        
        // Remove favourite from file, first read in list from file, remove from list and then write list back to file
        public static void RemoveFavouriteFromFile(RecipesData recipeObject)
        {
            // Create a new list of FavouriteRecipesData
            ObservableCollection<RecipesData> list = new ObservableCollection<RecipesData>();

            try
            {
                //read the local file 
                string fullPath = ReadFromFile(ref list, FAVOURITES_SAVE_FILE);

                // Scan through list, if recipe is found remove from list
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

                // write updated list back out to file
                WriteListToFile(list, fullPath);
            }
            catch
            { }
        }

        // Generic read from list from file, and get path of file
        private static string ReadFromFile<T>(ref ObservableCollection<T> list, string fileName)
        {
            string fileString, path, fullPath;

            //read the local file 
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            fullPath = Path.Combine(path, fileName);

            // If the file doesn't exist create the file
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath).Dispose();
            }
            else
            {
                // Otherwise read in file to list, list is updated in caller with ref
                using (var reader = new StreamReader(fullPath))
                {
                    fileString = reader.ReadToEnd();
                    list = JsonConvert.DeserializeObject<ObservableCollection<T>>(fileString);
                }
            }
            return fullPath;
        }

        // Generic write passed in ObservableCollection list to file method
        private static void WriteListToFile<T>(ObservableCollection<T> list, string fullPath)
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
