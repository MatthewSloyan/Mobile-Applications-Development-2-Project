using System;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByRecipe : ContentPage
    {
        bool bRecipeName = false;

        public SearchByRecipe()
        {
            InitializeComponent();
            btnSearch.IsEnabled = false;
        }
        
        // Check if first required entry box contains a value, if not disable button 
        private void EntRecipe_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Check for valid input
            if (entRecipe.Text == "")
            {
                bRecipeName = false;
                btnSearch.IsEnabled = false;
            }
            else { bRecipeName = true; }

            if (bRecipeName == true)
            {
                btnSearch.IsEnabled = true;
            }
        }

        // On search get information from entry boxes and make url to search with
        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            string dynamicString = "";

            if (entDiet.Text != null)
            {
                dynamicString += "diet="+ entDiet.Text.Trim() + "&";
            }

            // Excluded ingredients
            if (entExIngredients.Text != null)
            {
                // Check for comma replaces commas with "%2C+" if encounted and strips out any additional white space
                dynamicString += "excludeIngredients=" + checkForComma(entExIngredients.Text) + "&";
            }

            // Excluded intolerance
            if (entExIntolerances.Text != null)
            {
                dynamicString += "excludeIngredients=" + checkForComma(entExIntolerances.Text) + "&";
            }

            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/search?" + dynamicString + "number=15&offset=0&query=" + entRecipe.Text.Trim();

            // Navigate to page and set up model view
            Navigation.PushAsync(new SearchByRecipesListView(URL, 2));
        }

        // Check for a comma in the entered string, if found add "%2C+" operator for http requests
        private string checkForComma(string entryText)
        {
            // Replace comma with and operator
            string modifiedComma = entryText.Replace(','.ToString(), "%2C+");
            // Strip out any additional white space if a space is entered between entries
            string strippedString = modifiedComma.Replace(" ", string.Empty);

            return strippedString;
        }
    }
}