using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace G00348036
{
    public class RecipesViewModel : BaseViewModel
    {
        #region == Private global variables ==
        private string Url { get; set; }
        private int Selection { get; set; }

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
        
        // Selected Recipe
        private RecipesData _selectedRecipe;
        public RecipesData SelectedRecipe
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
            this.Url = URL;
            this.Selection = selection;

            getRecipeInfo();
        }

        private void getRecipeInfo()
        {
            // Starts a background task, which will load the page and then the data when ready. It creates a better user experience rather 
            // than having the app hang while the data is loaded.

            Task.Factory.StartNew(() =>
            {
                try
                {
                    //If 1 then call is from searchByIngredients or searchByImage, as the json returned can be parsed straight into a list
                    if (Selection == 1)
                    {
                        // Get list of results from api
                        Results = Utils.GetApiData<RecipeResults>(Url);

                        if (Results.Count == 0 || Results == null)
                        {
                            // If no results are returned display error
                            // For some reason this was causing an unhandled expection
                            //_pageService.DisplayAlert("Error", "No recipes found using your input ingredients, please try again.", "OK", "CANCEL");
                        }
                    }
                    else
                    {
                        //If 2 then call is from searchByRecipe, as the json returned is an object with a nested list which is then converted to a list object
                        SearchRecipesData RecipeResults = Utils.GetSingleApiData<SearchRecipesData>(Url);
                        if (RecipeResults.results.Count == 0 || RecipeResults.results == null)
                        {
                            //// For some reason this was causing an unhandled expection
                            //_pageService.DisplayAlert("Error", "No recipes found using your inputs, please try again.", "OK", "CANCEL");
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
                catch (Exception)
                {
                    // Throw general error message if something unexpected happens
                    _pageService.DisplayAlert("Error", "Error occured, please try again.", "OK", "CANCEL");
                }
            });
        }

        #region == Public method ==
        // Add recipe to favourites by passing object to utils class 
        public void AddToFavourites(RecipeResults s)
        {
            Utils.AddToFavourites(s);
        }

        // Removes recipe from list when swiped
        // It doesn't seem to always get the right object, as it could be due to the selected recipe passed in
        // I have left this out as it didn't meet the required standards
        //public void RemoveFromList(RecipeResults s)
        //{
        //    Results.Remove(s);
        //    SelectedRecipe = null;
        //}

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
