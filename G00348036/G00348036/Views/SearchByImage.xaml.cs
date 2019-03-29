using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Newtonsoft.Json;
using Plugin.Media;
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
            //ImageJsonData.Image i = new ImageJsonData.Image();
            //i.content = "image";

            //ImageJsonData.Feature f = new ImageJsonData.Feature
            //{
            //    type = "FACE_DETECTION",
            //    maxResults = 10
            //};

            ////List<ImageJsonData.Feature> features = new List<ImageJsonData.Feature>();
            ////features.Add(f);

            //ImageJsonData.Request r = new ImageJsonData.Request();
            //r.image = i;
            //r.features = new List<ImageJsonData.Feature>
            //{
            //    type = "FACE_DETECTION",
            //    maxResults = 10
            //};

            //List<ImageJsonData.Request> requests = new List<ImageJsonData.Request>();
            //requests.Add(r);

            //ImageJsonData.RootObject full = new ImageJsonData.RootObject();
            //full.requests.Add(r);

            var obj = new
            {
                requests = new[] {
                    new  {
                         image = new { content  = "image" },
                         features = new[] {
                             new  { type = "LABEL_DETECTION", maxResults = 10}
                         }
                    }
                }
            };

            JsonConvert.SerializeObject(obj);
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(obj).ToString());

            TakePictureAsync();
        }

        private async Task TakePictureAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            PhotoImage.Source = Xamarin.Forms.ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                setUpAPI(file.Path);
                return stream;
            });

            
        }

        private void setUpAPI(string path)
        {
            //var credential = GoogleCredential.FromFile("C:/Users/Matthew/Source/Repos/SearchByIngredientsApp-333bfa73dcc2.json");
            // Instantiates a client
            //var client = ImageAnnotatorClient.Create();
            //// Load the image file into memory
            //var image = Google.Cloud.Vision.V1.Image.FromFile(path);
            //// Performs label detection on the image file
            //var response = client.DetectLabels(image);
            //foreach (var annotation in response)
            //{
            //    if (annotation.Description != null)
            //    {
            //        Console.WriteLine(annotation.Description);
            //        System.Diagnostics.Debug.WriteLine(annotation.Description);
            //    }
            //}
            //var credential = GoogleCredential.GetApplicationDefault();
            //var credential = GoogleCredential.FromFile("C:/Users/Matthew/Source/Repos/SearchByIngredientsApp-333bfa73dcc2.json");


        }
    }
}