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
        //ArrayList list = new ArrayList();
       // int count = 0;

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
        }

        private void createAddIngredientInput()
        {
            var layout = new StackLayout();
            layout.Orientation = StackOrientation.Horizontal;
            layout.StyleId = "slAddIngredient";
            //list.Add(count++);

            var entIngredient = new Entry
            {
                Placeholder = "Enter Your Ingredient",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                StyleId = "entryIngredient"
            };

            var addButton = new Button
            {
                Text = "ADD",
                WidthRequest = 70,
                Style = (Style)Application.Current.Resources["ButtonStyleFull"]
            };

            var deleteButton = new Button
            {
                Text = "DEL",
                WidthRequest = 70,
                Style = (Style)Application.Current.Resources["ButtonStyleFull"],
                StyleId = slIngredients.Children.Count.ToString()
            };

            addButton.Clicked += BtnAdd_Clicked;
            deleteButton.Clicked += BtnDelete_Clicked;
            layout.Children.Add(entIngredient);
            layout.Children.Add(addButton);
            layout.Children.Add(deleteButton);
            slIngredients.Children.Insert(slIngredients.Children.Count - 1, layout);
        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            // Remove the last child from the stacklayout, which removes the entry and two buttons.
            // I tried to remove the exact index child to no avail.. Tries can be seen in previous commits
            // It's not the most ideal solution but it solves the problem of accidentally adding too many entries 
            if(slIngredients.Children.Count != 1) 
                slIngredients.Children.RemoveAt(slIngredients.Children.Count-1);

            //for (int i = 1; i <= slIngredients.Children.Count-1; i++)
            //{
            //    if (i.ToString() == (sender as Button).StyleId)
            //    {
            //        slIngredients.Children.RemoveAt(i);
            //        //count--;
            //        //System.Diagnostics.Debug.WriteLine(count);
            //    }
            //}
            //foreach (View item in slIngredients.Children)
            //{
            //    if (item.StyleId == "slAddIngredient")
            //    {
            //        var sl = (StackLayout)item;
                    
            //        foreach (View slItems in sl.Children)
            //        {
            //            //var sl = (StackLayout)item 
            //            if (i.ToString() == (sender as Button).StyleId)
            //            {
            //                slIngredients.Children.RemoveAt(i);
            //                count--;
            //                System.Diagnostics.Debug.WriteLine(count);

            //                foreach (View item in slIngredients.Children)
            //                {

            //                }
            //            }
            //        }
            //    }
            //}

            //if (slIngredients.Children.Count != 1)
            //{
            //    slIngredients.Children.RemoveAt(1);
            //}
        }

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

            // Strip out any white space just incase
            string strippedString = dynamicString.Replace(" ", string.Empty);

            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=15&ranking=1&fillIngredients=true&ingredients=" + strippedString;

            Navigation.PushAsync(new Recipes(URL, 1));
        }
    }
}