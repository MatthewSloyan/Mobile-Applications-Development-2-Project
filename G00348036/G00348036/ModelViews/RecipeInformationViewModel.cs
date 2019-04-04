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
        public ObservableCollection<InstructionStepsData> Instructions { get; set; }
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
                string url;
                url = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + Id + "/information";
                Result = Utils.GetSingleApiData<RecipeInformationData>(url);

                url = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + Id + "/analyzedInstructions?stepBreakdown=true";
                Instructions = Utils.GetApiData<InstructionStepsData>(url);
                Steps = Instructions[0].steps;
            }
            catch (Exception)
            {
            }
        }
    }
}
