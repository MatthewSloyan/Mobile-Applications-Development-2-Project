using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Favourites : ContentPage
	{
		public Favourites ()
		{
			InitializeComponent ();
        }

        // Calls when ever the page comes into view, this will update the favourites
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = new FavouritesViewModel(new PageService());
        }

        #region Event Handlers
        // When a list item is selected get the object of the recipe and send it's id to the RecipeInformation page to load full recipe
        private void LvFavourites_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            RecipesData i = e.SelectedItem as RecipesData;
            (BindingContext as FavouritesViewModel).NavigatePage(i.id);
        }

        // It button is selected get object and pass to viewmodel for deletion
        private void BtnDeleteFromFavourites_Clicked(object sender, EventArgs e)
        {
            // Get the object that was clicked
            RecipesData f = (sender as Button).CommandParameter as RecipesData;
            (BindingContext as FavouritesViewModel).RemoveFromFavourites(f);
        }
        #endregion
    }
}