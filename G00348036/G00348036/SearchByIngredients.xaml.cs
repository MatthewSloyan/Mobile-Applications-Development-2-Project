using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;
using unirest_net.request;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchByIngredients : ContentPage
	{
        //Global list
        ArrayList list;

        public SearchByIngredients ()
		{
			InitializeComponent ();
            setUpComponents();
		}

        private void setUpComponents()
        {
            //set up initial two input boxes
            //createAddIngredientInput();
           // createAddIngredientInput();

            list = new ArrayList();
        }

        private void EntIngredient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            createAddIngredientInput();
        }

        private void createAddIngredientInput()
        {
            var layout = new StackLayout();
            layout.Orientation = StackOrientation.Horizontal;
            //layout.StyleId = "slAddIngredient" + 1;
            list.Add("slAddIngredient");

            var entIngredient = new Entry
            {
                Placeholder = "Enter Your Ingredient",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var addButton = new Button
            {
                Text = "Add",
                WidthRequest = 60
            };

            var deleteButton = new Button
            {
                Text = "Del",
                WidthRequest = 60
            };

            addButton.Clicked += BtnAdd_Clicked;
            layout.Children.Add(entIngredient);
            layout.Children.Add(addButton);
            layout.Children.Add(deleteButton);
            slIngredients.Children.Add(layout);
        }

        private void BtnSearchByIngredient_Clicked(object sender, EventArgs e)
        {
            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=10&ranking=1&fillIngredients=true&ingredients=apples%2Cflour%2Csugar";

            List<SearchByIngredientsData> results = GetApiData<SearchByIngredientsData>(URL);

            System.Diagnostics.Debug.WriteLine(results[0].id);
        }

        private List<T> GetApiData<T>(string URL)
        {
            HttpRequest request = Unirest.get(URL)
                .header("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
            HttpResponse<string> response = request.asString();

            List<T> results = JsonConvert.DeserializeObject<List<T>>(response.Body);
            return results;
        }

        //public async Task<T> Get<T>(string url)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
        //        client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
        //        var json = await client.GetStringAsync(url);
        //        return JsonConvert.DeserializeObject<T>(json);
        //    }
        //}

        //private async Task<RootObject> GetRootInfo()
        //{
        //    HttpResponse<RootObject> response = await Unirest.get("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=10&ranking=1&fillIngredients=true&ingredients=apples%2Cflour%2Csugar")
        //    .header("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525")
        //    .header("Accept", "application/json")
        //    .asJsonAsync<RootObject>();

        //    return response.Body;
        //}


        //private BtnSearchByIngredient_Clicked(object sender, EventArgs e)
        //{
        //try
        //{
        //    string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=10&ranking=1&fillIngredients=true&ingredients=apples%2Cflour%2Csugar";

        //    HttpClient httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
        //    HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        var recipes = JsonConvert.DeserializeObject<RootObject>(content);

        //        List<SearchByIngredientsData> results = new List<SearchByIngredientsData>();

        //        results = recipes.results;
        //        System.Diagnostics.Debug.WriteLine(results);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    //log error
        //}

        //string s = entIngredient1.Text;



        //private async void BtnSearchByIngredient_ClickedAsync(object sender, EventArgs e)
        //{
        //    string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=10&ranking=1&fillIngredients=true&ingredients=apples%2Cflour%2Csugar";

        //    HttpClient httpClient = new HttpClient();

        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525");
        //    //HttpClient httpClient = new HttpClient(new HttpClientHandler { Credentials = new NetworkCredential("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525") });

        //    HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        var recipes = JsonConvert.DeserializeObject<RootObject>(content);

        //        List<SearchByIngredientsData> results = new List<SearchByIngredientsData>();

        //        results = recipes.results;
        //        System.Diagnostics.Debug.WriteLine(results);
        //    }
        //}
    }
}