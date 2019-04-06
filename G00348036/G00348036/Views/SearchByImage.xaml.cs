using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchByImage : ContentPage
	{
        string filePath = "";

        public SearchByImage ()
		{
			InitializeComponent ();
            btnSend.IsEnabled = false;
        }

        #region Event handlers
        private async void BtnTakePicture_Clicked(object sender, EventArgs e)
        {
            await TakePictureAsync();
            // After picture is taken enable search button
            btnSend.IsEnabled = true;
        }
        
        private void BtnSend_Clicked(object sender, EventArgs e)
        {
            // set up api call and load search results page
            SetUpAPI();
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
        
        // set up api call and load search results page
        private void SetUpAPI()
        {
            // Convert the saved image to base64 format to send via http
            byte[] bytes = File.ReadAllBytes(filePath);
            string file = Convert.ToBase64String(bytes);
            string result = "";

            // Based on the JSON format and layout that google Vision requires build Json data using information.
            // content = base64 image converted above
            // type = OBJECT_LOCALIZATION to find multiple objects in an image
            // maxResults = number of ingredients entered in picker
            var obj = new
            {
                requests = new[] {
                    new  {
                         image = new { content  = file },
                         features = new[] {
                             new  { type = "OBJECT_LOCALIZATION", maxResults = pckIngredients.SelectedIndex}
                         }
                    }
                }
            };

            try
            {
                // To implement the HTTP request I found suitable code below but modified it and improved for my own needs.
                // https://stackoverflow.com/questions/9145667/how-to-post-json-to-a-server-using-c

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://vision.googleapis.com/v1/images:annotate?key=AIzaSyAz2XlLIlDE4NHoCENCrliqW1Motsk8WHY");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    // Serialize the new object created above into JSON format for sending via HTTP
                    streamWriter.Write(JsonConvert.SerializeObject(obj));
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                // Recieve response from Google Vision API call, and pass into load method
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    loadRecipesPage(result);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error");
            }
        }

        // using the string of json data returned from Google Vision
        private void loadRecipesPage(string result)
        {
            string dynamicString = "";
            System.Diagnostics.Debug.WriteLine(result);

            // To extract the data from the json was tough as it contains two nested lists, so with trial and error I solved it.
            // First get object from json string
            SearchByImageApiData results = JsonConvert.DeserializeObject<SearchByImageApiData>(result);

            // Get first nested list
            List<Respons> response = new List<Respons>();
            response = results.responses;

            // get second nested list which is used to loop through
            List<LocalizedObjectAnnotation> listOfDetectedIngredients = new List<LocalizedObjectAnnotation>();
            listOfDetectedIngredients = response[0].localizedObjectAnnotations;

            // Iterate through the returned api data to the limit specified by the user
            for (int i = 0; i < pckIngredients.SelectedIndex; i++)
            {
                dynamicString += listOfDetectedIngredients[i].name + "%2C";
            }       
            
            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=15&ranking=1&fillIngredients=true&ingredients=" + dynamicString;

            System.Diagnostics.Debug.WriteLine(URL);

            Navigation.PushAsync(new Recipes(URL, 1));
        }
    }
}