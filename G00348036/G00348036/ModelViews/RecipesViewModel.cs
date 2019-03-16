using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    class RecipesViewModel
    {
        public string url { get; set; }

        //global list of recipes
        public List<SearchByIngredientsData> Results { get; set; }

        // Contructor
        public RecipesViewModel(string URL)
        {
            this.url = URL;
            getRecipeInfo();
        }

        private void getRecipeInfo()
        {
            Results = Utils.GetApiData<SearchByIngredientsData>(url);

            System.Diagnostics.Debug.WriteLine(Results[0].id);
        }
    }
}
