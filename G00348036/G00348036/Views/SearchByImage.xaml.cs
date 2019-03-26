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
	public partial class SearchByImage : ContentPage
	{
		public SearchByImage ()
		{
			InitializeComponent ();
		}

        private void BtnTakePicture_Clicked(object sender, EventArgs e)
        {

        }
    }
}