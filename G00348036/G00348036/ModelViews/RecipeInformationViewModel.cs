using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace G00348036
{
    class RecipeInformationViewModel
    {
        private string Id { get; set; }

        //global list of recipes
        public RecipeInformationData Result { get; set; }
        public List<ExtendedIngredient> ExtendedIngredients { get; set; }
        public List<Step> Steps { get; set; }

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
        }
    }
}
