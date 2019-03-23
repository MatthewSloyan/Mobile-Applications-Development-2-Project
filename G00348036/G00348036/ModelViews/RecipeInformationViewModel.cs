using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    class RecipeInformationViewModel
    {
        public string Id { get; set; }

        //global list of recipes
        public RecipeInformationData Result { get; set; }

        // Contructor
        public RecipeInformationViewModel(string Id)
        {
            this.Id = Id;
            getRecipeInfo();
        }

        private void getRecipeInfo()
        {
            string url = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/" + Id + "/information";
            Result = Utils.GetSingleApiData<RecipeInformationData>(url);

            //System.Diagnostics.Debug.WriteLine(Result.id);
        }
    }
}
