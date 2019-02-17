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

        }
    }
}