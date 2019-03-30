using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Favourites : ContentPage
	{
		public Favourites ()
		{
			InitializeComponent ();
            this.BindingContext = new FavouritesViewModel();
        }

        private void LvFavourites_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            FavouriteRecipesData i = e.SelectedItem as FavouriteRecipesData;

            Navigation.PushAsync(new RecipeInformation(i.id));
        }

        private void BtnDeleteFromFavourites_Clicked(object sender, EventArgs e)
        {
            // Get the object that was clicked
            FavouriteRecipesData f = (sender as Button).CommandParameter as FavouriteRecipesData;
            (BindingContext as FavouritesViewModel).RemoveFromFavourites(f);
        }
    }
}