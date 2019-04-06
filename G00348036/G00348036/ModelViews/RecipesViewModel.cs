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

        //global list of Recipes for search by ingredients
        private ObservableCollection<RecipeResults> _results;
        public ObservableCollection<RecipeResults> Results
        {
            get { return _results; }
            set { SetValue(ref _results, value); }
        }

        //global list of recipes for search by recipe
        public ObservableCollection<RecipeResults> _recipeResultsConverted;
        public ObservableCollection<RecipeResults> RecipeResultsConverted
        {
            get { return _results; }
            set { SetValue(ref _results, value); }
        }
        
        // Selected ingredient
        private RecipeResults _selectedRecipe;
        public RecipeResults SelectedRecipe
        {
            get { return _selectedRecipe; }
            set { SetValue(ref _selectedRecipe, value); }
        }
        
        // Page service interface
        private readonly IPageService _pageService;

        // Contructor
        public RecipesViewModel(IPageService pageService, string URL, int selection)
        {
            _pageService = pageService;
            this.url = URL;
            this.selection = selection;

            getRecipeInfo();
        }

        private void getRecipeInfo()
        {
            //If one then call is from searchByIngredients
            if (selection == 1)
            {
                Results = Utils.GetApiData<RecipeResults>(url);
            }
            else
            {
                SearchRecipesData RecipeResults = Utils.GetSingleApiData<SearchRecipesData>(url);
                RecipeResultsConverted = RecipeResults.results;

                // As image url doesn't include the base URL prepend onto image for displaying.
                for (int i = 0; i < RecipeResultsConverted.Count; ++i)
                {
                    RecipeResultsConverted[i].image = "https://spoonacular.com/recipeImages/" + RecipeResultsConverted[i].image;
                }
            }
        }

        public void AddToFavourites(RecipeResults s)
        {
            Utils.AddToFavourites(s);
        }

        public void RemoveFromList(RecipeResults s)
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
