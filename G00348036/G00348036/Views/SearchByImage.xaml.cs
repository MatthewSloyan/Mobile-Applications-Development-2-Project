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
		public SearchByImage ()
		{
			InitializeComponent ();
		}

        private void BtnTakePicture_Clicked(object sender, EventArgs e)
        {
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
                Name = "image.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            PhotoImage.Source = Xamarin.Forms.ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();

                // Set up the API call
                SetUpAPI(file.Path);
                return stream;
            });
        }

        private void SetUpAPI(string path)
        {
            // Convert the saved image to base64 format to send via http
            byte[] bytes = File.ReadAllBytes(path);
            string file = Convert.ToBase64String(bytes);

            // Based on the JSON format and layout that google Vision requires build Json data using information.
            var obj = new
            {
                requests = new[] {
                    new  {
                         image = new { content  = file },
                         features = new[] {
                             new  { type = "OBJECT_LOCALIZATION", maxResults = 10}
                         }
                    }
                }
            };

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

            // Recieve response from Google Vision API call
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(result);
                SearchByImageApiData results = JsonConvert.DeserializeObject<SearchByImageApiData>(result);

                // Test code for trying to access data in class

                //List<LocalizedObjectAnnotation> yourValues = new List<LocalizedObjectAnnotation>();
                ////yourValues.nam

                //SearchByImageApiData _model = new SearchByImageApiData();
                //foreach (var item in _model.responses)
                //{
                //    item.localizedObjectAnnotations = yourValues;
                //}

                //SearchByImageApiData results = JsonConvert.DeserializeObject<SearchByImageApiData>(result);

                //SearchByRecipeData.Result selected = e.SelectedItem as SearchByRecipeData.Result;

                //List<LocalizedObjectAnnotation> localizedObjectAnnotations = new List<LocalizedObjectAnnotation>();
                //localizedObjectAnnotations = results.responses;

                //List<LocalizedObjectAnnotation> localizedObjectAnnotations = new List<LocalizedObjectAnnotation>();

                //Respons g = new Respons();

                //foreach (Respons i in results.responses)
                //{
                //    i.localizedObjectAnnotations.Add("");
                //    System.Diagnostics.Debug.WriteLine(i.localizedObjectAnnotations);
                //}

                //System.Diagnostics.Debug.WriteLine(results.name);

                //for (int i = 0; i < results.responses.Count; i++)
                //{
                //    List<LocalizedObjectAnnotation> r = new List<LocalizedObjectAnnotation>();
                //    r = r.IndexOf(i);
                //    Console.WriteLine("Item in a!");
                //    for (int j = 0; j < r.Count; j++)
                //    {
                //        Console.WriteLine("Item in b!");
                //    }
                //}


                //foreach (object i in results.responses)
                //{
                //    foreach (object j in i)
                //    {

                //    }
                //        System.Diagnostics.Debug.WriteLine(i);
                //}
            }
        }
    }
}