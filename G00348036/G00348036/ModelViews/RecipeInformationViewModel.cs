using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace G00348036
{
    class RecipeInformationViewModel
    {
        public string Id { get; set; }

        //global list of recipes
        public RecipeInformationData Result { get; set; }
        public List<ExtendedIngredient> ExtendedIngredients { get; set; }
        public List<Step> Steps { get; set; }

        // Contructor
        public RecipeInformationViewModel(string Id)
        {
            this.Id = Id;
            getRecipeInfo();
        }

        private void getRecipeInfo()
        {
            try
            {
                string url = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + Id + "/information";
                Result = Utils.GetSingleApiData<RecipeInformationData>(url);
                ExtendedIngredients = Result.extendedIngredients;

                // As image url doesn't include the base URL prepend onto image for displaying.
                for (int i = 0; i < ExtendedIngredients.Count; ++i)
                {
                    ExtendedIngredients[i].image = "https://spoonacular.com/cdn/ingredients_100x100/" + ExtendedIngredients[i].image;
                }

                string urlIntructions = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + Id + "/analyzedInstructions?stepBreakdown=true";
                ObservableCollection<InstructionStepsData> Instructions = Utils.GetApiData<InstructionStepsData>(urlIntructions);
                Steps = Instructions[0].steps;
            }
            catch (Exception)
            {
            }
        }
    }
}
