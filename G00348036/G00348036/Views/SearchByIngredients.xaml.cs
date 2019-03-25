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

        public SearchByIngredients ()
		{
			InitializeComponent ();
            setUpComponents();

            this.BindingContext = new SearchByIngredientsViewModel();
        }

        private void setUpComponents()
        {
            //list = new ArrayList();
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
            string dynamicString = "";

            // Iterate through each child of the overall stacklayout
            foreach (View item in slIngredients.Children)
            {
                // If it's a stacklayout then get set as variable
                if (item.StyleId == "slAddIngredient")
                {
                    var sl = (StackLayout)item;

                    // Iterate through each child of the inner stacklayout which contains the entry and button
                    foreach (View slItems in sl.Children)
                    {
                        // If it's a entry then get text and add to arrayList
                        if (slItems.StyleId == "entryIngredient")
                        {
                            var entry = (Entry)slItems;
                            dynamicString += entry.Text + "%2C";
                        }
                    }
                }
            }
            
            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=10&ranking=1&fillIngredients=true&ingredients=" + dynamicString;

            Navigation.PushAsync(new Recipes(URL));
        }
    }
}