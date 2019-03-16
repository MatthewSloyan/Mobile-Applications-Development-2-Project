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

            this.BindingContext = new SearchByIngredientsViewModel();
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

            //var layout = new StackLayout();
            //layout = (BindingContext as SearchByIngredientsViewModel).createAddIngredientInput();
            //slIngredients.Children.Add(layout);
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
                WidthRequest = 70
            };

            var deleteButton = new Button
            {
                Text = "Del",
                WidthRequest = 70
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

            //List<SearchByIngredientsData> results = GetApiData<SearchByIngredientsData>(URL);
            //System.Diagnostics.Debug.WriteLine(results[0].id);

            Navigation.PushAsync(new Recipes(URL));
        }
    }
}