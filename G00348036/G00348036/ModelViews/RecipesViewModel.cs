﻿using System;
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

        public SearchByRecipeData RecipeResults { get; set; } = null;
        public List<SearchByRecipeData.Result> RecipeResultsConverted { get; set; } = null;

        // Contructor
        public RecipesViewModel(string URL, int selection)
        {
            this.url = URL;
            this.selection = selection;
            getRecipeInfo();
        }

        private void getRecipeInfo()
        {
            //If one then call is from searchByIngredients
            if (selection == 1)
            {
                Results = Utils.GetApiData<SearchByIngredientsData>(url); 
            }
            else
            {
                RecipeResults = Utils.GetSingleApiData<SearchByRecipeData>(url);
                RecipeResultsConverted = RecipeResults.results;

                // As image url doesn't include the base URL prepend onto image for displaying.
                for (int i = 0; i < RecipeResultsConverted.Count; ++i)
                {
                    RecipeResultsConverted[i].image = "https://spoonacular.com/recipeImages/" + RecipeResultsConverted[i].image;
                }
            }
            
        }
    }
}
