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
                dynamicString += "diet="+ entDiet.Text + "&";
            }

            if (entExIngredients.Text != null)
            {
                dynamicString += "excludeIngredients=" + entExIngredients.Text + "&";
            }

            if (entExIntolerances.Text != null)
            {
                dynamicString += "excludeIngredients=" + entExIntolerances.Text + "&";
            }

            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/search?" + dynamicString + "number=10&offset=0&query=" + entRecipe.Text;

            Navigation.PushAsync(new SearchByRecipesListView(URL, 2));
        }
    }
}