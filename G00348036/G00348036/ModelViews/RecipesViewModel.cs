using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    class RecipesViewModel
    {
        private string url { get; set; }
        private int selection { get; set; }

        //global list of recipes
        public List<SearchByIngredientsData> Results { get; set; } = null;
        public List<SearchByRecipeData> RecipeResults { get; set; } = null;

        // Contructor
        public RecipesViewModel(string URL, int selection)
        {
            this.url = URL;
            this.selection = selection;
            getRecipeInfo();
        }

        private void getRecipeInfo()
        {
            if (selection == 1)
            {
                Results = Utils.GetApiData<SearchByIngredientsData>(url);
            }
            else
            {
                RecipeResults = Utils.GetApiData<SearchByRecipeData>(url);
            }
            
            System.Diagnostics.Debug.WriteLine(Results[0].id);
        }
    }
}
