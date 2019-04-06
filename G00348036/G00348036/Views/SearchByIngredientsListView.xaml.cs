﻿using G00348036.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchByIngredientsListView : ContentPage
	{
        public SearchByIngredientsListView(string URL, int selection)
		{
			InitializeComponent ();
            this.BindingContext = new RecipesViewModel(new PageService(), URL, selection);
		}

        #region Event Handlers
        // When a list item is selected get the object of the recipe and send it's id to the RecipeInformation page to load full recipe
        private void LvRecipes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            RecipeResults i = e.SelectedItem as RecipeResults;
            (BindingContext as RecipesViewModel).NavigatePage(i.id);
        }

        // When button is clicked get object from list and pass to view model
        private void BtnAddToFavourites_Clicked(object sender, System.EventArgs e)
        {
            RecipeResults s = (sender as Button).CommandParameter as RecipeResults;

            (BindingContext as RecipesViewModel).AddToFavourites(s);
        }

        // I wanted to implement a quick way to get through the list, so swiping will remove the recipe from the list
        // but from testing it's not the most intutitive feature but it still works.
        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            // Get the object that was swiped and remove from list
            RecipeResults s = e.Parameter as RecipeResults;
            (BindingContext as RecipesViewModel).RemoveFromList(s);
        }
        #endregion
    }
}