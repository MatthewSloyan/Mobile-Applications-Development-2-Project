using System.Collections.ObjectModel;

namespace G00348036
{
    public class RecipesViewModel : BaseViewModel
    {
        #region == Private global variables ==
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
            get { return _recipeResultsConverted; }
            set { SetValue(ref _recipeResultsConverted, value); }
        }
        
        // Selected ingredient
        private RecipeResults _selectedRecipe;
        public RecipeResults SelectedRecipe
        {
            get { return _selectedRecipe; }
            set { SetValue(ref _selectedRecipe, value); }
        }
        
        // Page service interface, set up in contructor
        private readonly IPageService _pageService;
        #endregion

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
            //If 1 then call is from searchByIngredients or searchByImage, as the json returned can be parsed straight into a list
            if (selection == 1)
            {
                // Get list of results from api
                Results = Utils.GetApiData<RecipeResults>(url);

                if (Results.Count == 0)
                {
                    // If no results are returned display error
                    _pageService.DisplayAlert("Error", "No recipes found using your input ingredients, please try again.", "OK", "CANCEL");
                }
            }
            else
            {
                //If 2 then call is from searchByRecipe, as the json returned is an object with a nested list which is then converted to a list object
                SearchRecipesData RecipeResults = Utils.GetSingleApiData<SearchRecipesData>(url);
                if (RecipeResults.results.Count == 0)
                {
                    _pageService.DisplayAlert("Error", "No recipes found using your inputs, please try again.", "OK", "CANCEL");
                    return;
                }

                // Get the nested list from the results and set as global variable 
                RecipeResultsConverted = RecipeResults.results;

                // As image url doesn't include the base URL prepend onto image for displaying.
                for (int i = 0; i < RecipeResultsConverted.Count; ++i)
                {
                    RecipeResultsConverted[i].image = "https://spoonacular.com/recipeImages/" + RecipeResultsConverted[i].image;
                }
            }
        }

        #region == Public method ==
        // Add recipe to favourites by passing object to utils class 
        public void AddToFavourites(RecipeResults s)
        {
            Utils.AddToFavourites(s);
        }

        // Removes recipe from list when swiped
        // It doesn't seem to always get the right object, as it could be due to the selected recipe passed in
        public void RemoveFromList(RecipeResults s)
        {
            Results.Remove(s);
            SelectedRecipe = null;
        }

        // Navigate using page service
        public void NavigatePage(string id)
        {
            // Navigation won't work, not a property of the view model
            // Part of the page class in Xamarin forms, use an interface
            _pageService.PushAsync(new RecipeInformation(id));
        }
        #endregion
    }
}
