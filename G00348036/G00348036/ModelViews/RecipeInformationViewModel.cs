using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace G00348036
{
    class RecipeInformationViewModel : BaseViewModel
    {
        private string Id { get; set; }

        //global list of recipes
        // Needed to call the set value method in the BaseViewModel to allow it to change the values if changed,
        // This allows the data to load once it's ready improving the user experience
        private RecipeInformationData _results;
        public RecipeInformationData Result
        {
            get { return _results; }
            set { SetValue(ref _results, value); }
        }
        
        private List<ExtendedIngredient> _extendedIngredients;
        public List<ExtendedIngredient> ExtendedIngredients
        {
            get { return _extendedIngredients; }
            set { SetValue(ref _extendedIngredients, value); }
        }

        private List<Step> _steps;
        public List<Step> Steps
        {
            get { return _steps; }
            set { SetValue(ref _steps, value); }
        }

        // Page service interface, set up in contructor
        private readonly IPageService _pageService;

        // Contructor
        public RecipeInformationViewModel(IPageService pageService, string Id)
        {
            _pageService = pageService;
            this.Id = Id;
            getRecipeInfo();
        }

        // Load the recipe information from the api
        private void getRecipeInfo()
        {
            // Starts a background task, which will load the page and then the data when ready. It creates a better user experience rather 
            // than having the app hang while the data is loaded.
            Task.Factory.StartNew(() =>
            {
                try
                {
                    // Get overall recipe information using the url and passed in id
                    string url = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + Id + "/information";
                    Result = Utils.GetSingleApiData<RecipeInformationData>(url);
                    ExtendedIngredients = Result.extendedIngredients;

                    // As image url doesn't include the base URL prepend onto image for displaying.
                    for (int i = 0; i < ExtendedIngredients.Count; ++i)
                    {
                        ExtendedIngredients[i].image = "https://spoonacular.com/cdn/ingredients_100x100/" + ExtendedIngredients[i].image;
                    }

                    // Get specific intructions with steps, as the above information doesn't include great instructions
                    string urlIntructions = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + Id + "/analyzedInstructions?stepBreakdown=true";
                    ObservableCollection<InstructionStepsData> Instructions = Utils.GetApiData<InstructionStepsData>(urlIntructions);
                    Steps = Instructions[0].steps;
                }
                catch (Exception)
                {
                    // If exception display error
                    _pageService.DisplayAlert("Error", "No recipe found, please try again.", "OK", "CANCEL");
                }
            });
        }
    }
}
