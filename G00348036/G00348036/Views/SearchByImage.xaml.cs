using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchByImage : ContentPage
	{
        string filePath = "";

        public SearchByImage ()
		{
			InitializeComponent ();
            this.BindingContext = new ImageViewModel(new PageService());
            btnSend.IsEnabled = false;
        }

        #region == Event handlers == 
        private async void BtnTakePicture_Clicked(object sender, EventArgs e)
        {
            await TakePictureAsync();
            // After picture is taken enable search button
            btnSend.IsEnabled = true;
        }
        
        private void BtnSend_Clicked(object sender, EventArgs e)
        {
            // set up api call and load search results page using model view
            (BindingContext as ImageViewModel).SetUpAPI(filePath, pckIngredients.SelectedIndex + 1);
        }
        #endregion

        // Camera functionality has been implemented and edited using the tutorial provided in the plugin documentation
        // Other setup for android was also required
        // https://github.com/jamesmontemagno/MediaPlugin
        private async Task TakePictureAsync()
        {
            await CrossMedia.Current.Initialize();

            // If camera is not available return, e.g if device doesn't contain camera
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }

            // take photo and save locally in application data 
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Images",
                Name = "image.jpg"
            });

            if (file == null)
                return;
            
            // Set image source on xaml to the new photo
            PhotoImage.Source = Xamarin.Forms.ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                // Set global file path
                filePath = file.Path;
                return stream;
            });
        }
    }
}