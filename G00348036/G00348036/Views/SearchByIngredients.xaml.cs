using System;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchByIngredients : ContentPage
	{
        //Global list
        ArrayList list;
        int count = 0;

        public SearchByIngredients ()
		{
			InitializeComponent ();
            setUpComponents();

            this.BindingContext = new SearchByIngredientsViewModel();
        }

        private void setUpComponents()
        {
            list = new ArrayList();
            createAddIngredientInput();
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
            layout.StyleId = "slAddIngredient";
            list.Add(count++);

            var entIngredient = new Entry
            {
                Placeholder = "Enter Your Ingredient",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                StyleId = "entryIngredient"
            };

            var addButton = new Button
            {
                Text = "Add",
                WidthRequest = 70
            };

            //var deleteButton = new Button
            //{
            //    Text = "Del",
            //    WidthRequest = 70
            //};

            addButton.Clicked += BtnAdd_Clicked;
            //deleteButton.Clicked += BtnDelete1_Clicked;
            layout.Children.Add(entIngredient);
            layout.Children.Add(addButton);
            //layout.Children.Add(deleteButton);
            slIngredients.Children.Add(layout);
        }

        //private void BtnDelete1_Clicked(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < list.Count; ++i)
        //    {
        //        if (list[i].ToString() == (sender as Button).StyleId)
        //        {
        //            slIngredients.Children.RemoveAt(i);
        //            Console.WriteLine(list[i]);
        //        }
               
        //    }

        //    //if (slIngredients.Children.Count != 1)
        //    //{
        //    //    slIngredients.Children.RemoveAt(1);
        //    //}
        //}

        private void BtnSearchByIngredient_Clicked(object sender, EventArgs e)
        {
            ArrayList ingredientsList = new ArrayList();

            foreach (View item in slIngredients.Children)
            {
                if (item.StyleId == "slAddIngredient")
                {
                    var sl = (StackLayout)item;

                    foreach (View slItems in sl.Children)
                    {
                        if (slItems.StyleId == "entryIngredient")
                        {
                            var entry = (Entry)slItems;
                            ingredientsList.Add(entry.Text);
                        }
                    }
                }
            }

            //slIngredients.Children.
            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=10&ranking=1&fillIngredients=true&ingredients=apples%2Cflour%2Csugar";

            //List<SearchByIngredientsData> results = GetApiData<SearchByIngredientsData>(URL);
            //System.Diagnostics.Debug.WriteLine(results[0].id);

            Navigation.PushAsync(new Recipes(URL));
        }
    }
}