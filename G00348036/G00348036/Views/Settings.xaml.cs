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
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
			InitializeComponent ();
		}

        private void SwitchVibration_Toggled(object sender, ToggledEventArgs e)
        {
            // Set the value of vibration to either true or false
            Application.Current.Properties["vibration"] = e.Value;
        }
    }
}