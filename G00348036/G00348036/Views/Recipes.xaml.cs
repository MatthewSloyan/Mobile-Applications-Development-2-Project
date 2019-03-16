using G00348036.Views;
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
	public partial class Recipes : ContentPage
	{
        public Recipes (string URL)
		{
			InitializeComponent ();
            this.BindingContext = new RecipesViewModel(URL);
		}

        private void LvRecipes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new RecipeInformation());
        }
    }
}