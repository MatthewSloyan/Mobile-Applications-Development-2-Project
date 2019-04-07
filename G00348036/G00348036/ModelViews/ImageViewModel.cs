using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace G00348036
{
    class ImageViewModel
    {
        // Page service interface
        private readonly IPageService _pageService;

        // Contructor
        public ImageViewModel(IPageService pageService)
        {
            _pageService = pageService;
        }

        // set up api call and load search results page
        public void SetUpAPI(string filePath, int pickerNumber)
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
                             new  { type = "OBJECT_LOCALIZATION", maxResults = pickerNumber}
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
                    // Handle error if no results are returned
                    if (result == "")
                    {
                        _pageService.DisplayAlert("Error", "No ingredients found in image, please try again.", "OK", "CANCEL");
                    }
                    else
                    {
                        loadRecipesPage(result, pickerNumber);
                    }
                }
            }
            catch (Exception)
            {
                _pageService.DisplayAlert("Error", "Error while detecting ingredients, please try again.", "OK", "CANCEL");
            }
        }

        // using the string of json data returned from Google Vision
        public void loadRecipesPage(string result, int pickerNumber)
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
            for (int i = 0; i < pickerNumber; i++)
            {
                dynamicString += listOfDetectedIngredients[i].name + "%2C";
            }

            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=15&ranking=1&fillIngredients=true&ingredients=" + dynamicString;

            System.Diagnostics.Debug.WriteLine(URL);
            
            _pageService.PushAsync(new SearchByIngredientsListView(URL, 1));
        }
    }
}
