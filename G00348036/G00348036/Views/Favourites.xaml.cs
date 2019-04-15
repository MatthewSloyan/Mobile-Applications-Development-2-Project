using System;
using System.IO;
using System.Reflection;
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
            // From testing I found that on pressing the back button the selected value would be null which would cause the app to crash
            // To fix this I have implemented a return if null which seems to work perfectly.
            if (e.SelectedItem == null)
            {
                return;
            }

            RecipesData i = e.SelectedItem as RecipesData;
            (BindingContext as FavouritesViewModel).NavigatePage(i.id);
        }

        // It button is selected get object and pass to viewmodel for deletion
        private void BtnDeleteFromFavourites_Clicked(object sender, EventArgs e)
        {
            // Get the object that was clicked
            RecipesData f = (sender as Button).CommandParameter as RecipesData;
            (BindingContext as FavouritesViewModel).RemoveFromFavourites(f);

            // Check if vibration property exists
            if (Application.Current.Properties.ContainsKey("sound"))
            {
                // Get value set
                string id = Application.Current.Properties["sound"].ToString();

                // If true then sound is on so play sound
                if (id == "True")
                {
                    try
                    {
                        // Read the sound file from the assets folder
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                        Stream stream = assembly.GetManifestResourceStream("G00348036.Assets.erasesound.wav");

                        var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                        player.Load(stream);
                        player.Play();
                    }
                    catch (Exception){}
                }
            }
        }
        #endregion
    }
}