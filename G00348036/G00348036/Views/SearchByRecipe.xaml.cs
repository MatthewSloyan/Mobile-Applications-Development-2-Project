using System;

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

            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/search?query=burger";

            Navigation.PushAsync(new Recipes(URL));
        }
    }
}