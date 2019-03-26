using G00348036.Views;

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
    }
}