using G00348036.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace G00348036
{
    public class RecipesViewModel : BaseViewModel
    {
        private string url { get; set; }
        private int selection { get; set; }

        //global list of Recipes for ingredients
        private ObservableCollection<SearchByIngredientsData> _results;
        public ObservableCollection<SearchByIngredientsData> Results
        {
            get { return _results; }
            set { SetValue(ref _results, value); }
        }

        private SearchByIngredientsData _selectedRecipe;
        public SearchByIngredientsData SelectedRecipe
        {
            get { return _selectedRecipe; }
            set { SetValue(ref _selectedRecipe, value); }
        }

        //global list of recipes
        public SearchByRecipeData RecipeResults { get; set; } = null;
        public List<SearchByRecipeData.Result> RecipeResultsConverted { get; set; } = null;

        // use command interfaces to "bind" commands from ui elements to a method in the view model
        // ICommand interface defines two methods
        // Execute - takes and action to be invoked/executed, essentially a method to run
        // initalised in the constructor
        //public ICommand AddToFavouritesCommand { get; set; }
        // set in the constructor
        private readonly IPageService _pageService;

        // Contructor
        public RecipesViewModel(IPageService pageService, string URL, int selection)
        {
            _pageService = pageService;
            this.url = URL;
            this.selection = selection;
            getRecipeInfo();

            // set up command as a new command, and pass in name of method as a param.
            // in Xamarin, the ui elements see the ICommand object and calls the execute method to invoke action
            //AddToFavouritesCommand = new Command<SearchByIngredientsData>(AddToFavourites);
        }

        private void getRecipeInfo()
        {
            //If one then call is from searchByIngredients
            if (selection == 1)
            {
                Results = Utils.GetApiData<SearchByIngredientsData>(url); 
            }
            else
            {
                RecipeResults = Utils.GetSingleApiData<SearchByRecipeData>(url);
                RecipeResultsConverted = RecipeResults.results;

                // As image url doesn't include the base URL prepend onto image for displaying.
                for (int i = 0; i < RecipeResultsConverted.Count; ++i)
                {
                    RecipeResultsConverted[i].image = "https://spoonacular.com/recipeImages/" + RecipeResultsConverted[i].image;
                }
            }
        }

        public void AddToFavourites(SearchByIngredientsData s)
        {
            Utils.AddToFavourites(s);
        }

        public void AddToFavouritesR(SearchByRecipeData.Result s)
        {
            Utils.AddToFavouritesR(s);
        }

        public void RemoveFromList(SearchByIngredientsData s)
        {
            Results.Remove(s);
            SelectedRecipe = null;
        }

        public void NavigatePage(string id)
        {
            // Navigation won't work, not a property of the view model
            // Part of the page class in Xamarin forms, use an interface
            _pageService.PushAsync(new RecipeInformation(id));
        }
    }
}
