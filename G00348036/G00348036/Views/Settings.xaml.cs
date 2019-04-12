
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

            CheckIfSoundIsOn();
        }

        private void CheckIfSoundIsOn()
        {
            // Check if vibration property exists
            if (Application.Current.Properties.ContainsKey("vibration"))
            {
                // Get value
                string id = Application.Current.Properties["vibration"].ToString();
                
                // If true then sound is on
                if (id == "True")
                {
                    switchVibration.IsToggled = true;
                }
                else
                {
                    switchVibration.IsToggled = false;
                }
            }
        }

        private void SwitchVibration_Toggled(object sender, ToggledEventArgs e)
        {
            // Set the value of vibration to either true or false
            Application.Current.Properties["vibration"] = e.Value;
        }
    }
}