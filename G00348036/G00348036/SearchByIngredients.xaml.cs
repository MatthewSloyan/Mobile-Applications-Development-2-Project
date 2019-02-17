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
	public partial class SearchByIngredients : ContentPage
	{
		public SearchByIngredients ()
		{
			InitializeComponent ();
		}

        private void EntIngredient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            var layout = new StackLayout();
            layout.Orientation = StackOrientation.Horizontal; 

            var entIngredient = new Entry
            {
                Placeholder = "Enter Your Ingredient",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var button = new Button
            {
                Text = "Add",
                WidthRequest = 50
            };

            layout.Children.Add(entIngredient);
            layout.Children.Add(button);
            slIngredients.Children.Add(layout);
        }

        private void BtnSearchByIngredient_Clicked(object sender, EventArgs e)
        {

        }
    }
}