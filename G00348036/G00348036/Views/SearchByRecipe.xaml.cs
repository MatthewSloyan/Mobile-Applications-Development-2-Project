using System;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByRecipe : ContentPage
    {
        public SearchByRecipe()
        {
            InitializeComponent();
        }

        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            string dynamicString = "";

            if (entDiet.Text != null)
            {
                dynamicString += "diet="+ entDiet.Text.Trim() + "&";
            }

            if (entExIngredients.Text != null)
            {
                dynamicString += "excludeIngredients=" + checkForComma(entExIngredients.Text) + "&";
            }

            if (entExIntolerances.Text != null)
            {
                dynamicString += "excludeIngredients=" + checkForComma(entExIntolerances.Text) + "&";
            }

            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/search?" + dynamicString + "number=15&offset=0&query=" + entRecipe.Text.Trim();

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