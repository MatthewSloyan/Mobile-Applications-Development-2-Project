using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;
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
            string s = entIngredient1.Text;

            Task<HttpResponse<SearchByIngredientsData>> response = Unirest.get("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=10&ranking=1&fillIngredients=true&ingredients=apples%2Cflour%2Csugar")
            .header("X-RapidAPI-Key", "583ced2f01mshf4b63cc4f7b49f7p130fc5jsn4e92edadc525")
            .asJsonAsync<SearchByIngredientsData>();
            //.asJson();
        }
    }
}