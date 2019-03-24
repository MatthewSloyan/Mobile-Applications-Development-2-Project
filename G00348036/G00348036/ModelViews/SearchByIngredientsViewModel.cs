using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace G00348036
{
    class SearchByIngredientsViewModel
    {
        #region == public commands ==
        // use command interfaces to "bind" commands from ui elements to a method in the view model
        // ICommand interface defines two methods
        // Execute - takes and action to be invoked/executed, essentially a method to run
        // initalised in the constructor
        public ICommand LoadRecipePageCommand { get; set; }
        #endregion

        // Contructors
        public SearchByIngredientsViewModel()
        {
            // set up command as a new command, and pass in name of method as a param.
            // in Xamarin, the ui elements see the ICommand object and calls the execute method to invoke action
            LoadRecipePageCommand = new Command(LoadRecipePage);
        }

        private void LoadRecipePage(object obj)
        {
            //string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=10&ranking=1&fillIngredients=true&ingredients=apples%2Cflour%2Csugar";

            //List<SearchByIngredientsData> results = GetApiData<SearchByIngredientsData>(URL);
            //System.Diagnostics.Debug.WriteLine(results[0].id);  

            //Navigation.PushAsync(new Recipes(URL));
        }
    }
}
