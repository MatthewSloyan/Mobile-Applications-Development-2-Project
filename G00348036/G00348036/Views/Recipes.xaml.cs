﻿using G00348036.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Recipes : ContentPage
	{
        public Recipes (string URL, int selection)
		{
			InitializeComponent ();
            this.BindingContext = new RecipesViewModel(URL, selection);
		}

        private void LvRecipes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SearchByIngredientsData i = e.SelectedItem as SearchByIngredientsData;

            Navigation.PushAsync(new RecipeInformation(i.id)); 
        }

        private void BtnAddToFavourites_Clicked(object sender, System.EventArgs e)
        {
            SearchByIngredientsData s = (sender as Button).CommandParameter as SearchByIngredientsData;

            (BindingContext as RecipesViewModel).AddToFavourites(s);
        }
    }
}