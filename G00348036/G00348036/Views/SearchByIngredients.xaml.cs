using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchByIngredients : ContentPage
	{
        public SearchByIngredients ()
		{
			InitializeComponent ();

            // Create one initial entry box and buttons
            createAddIngredientInput();
        }
        
        private void createAddIngredientInput()
        {
            var layout = new StackLayout();
            layout.Orientation = StackOrientation.Horizontal;

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
                // Get style from global app.xaml page
                Style = (Style)Application.Current.Resources["ButtonStyleFull"]
            };

            var deleteButton = new Button
            {
                Text = "DEL",
                WidthRequest = 70,
                Style = (Style)Application.Current.Resources["ButtonStyleFull"],
                BackgroundColor = Color.FromHex("e55050")
            };

            // Add clicked events
            addButton.Clicked += BtnAdd_Clicked;
            deleteButton.Clicked += BtnDelete_Clicked;
            // Add all elements to stacklayout and then to outer stacklayout
            layout.Children.Add(entIngredient);
            layout.Children.Add(addButton);
            layout.Children.Add(deleteButton);
            slIngredients.Children.Add(layout);
        }

        #region Event handlers
        // On add button click add new child to stacklayout
        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            createAddIngredientInput();
        }

        // On del button click remove last child from stacklayout
        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            // Remove the last child from the stacklayout, which removes the entry and two buttons.
            // I tried to remove the exact index child to no avail.. Tries can be seen in previous commits
            // It's not the most ideal solution but it solves the problem of accidentally adding too many entries 
            if(slIngredients.Children.Count != 1) 
                slIngredients.Children.RemoveAt(slIngredients.Children.Count-1);
        }

        // Search for recipes using the text entries
        private void BtnSearchByIngredient_Clicked(object sender, EventArgs e)
        {
            string dynamicString = "";

            // Iterate through each child of the overall stacklayout in xaml then
            // Get stacklayout as it can only contain stacklayouts
            foreach (View outerStacklayoutItem in slIngredients.Children)
            {
                var sl = (StackLayout)outerStacklayoutItem;

                // Iterate through each child of the inner stacklayout which contains the entry and buttons
                foreach (View innerStacklayoutItem in sl.Children)
                {
                    // If it's a entry then get text and add to string with and operator
                    if (innerStacklayoutItem.StyleId == "entryIngredient")
                    {
                        var entry = (Entry)innerStacklayoutItem;
                        if (entry.Text != "")
                        {
                            dynamicString += entry.Text.Trim() + "%2C";
                        }
                    }
                } 
            }

            // Strip out any white space for extra safety
            string strippedString = dynamicString.Replace(" ", string.Empty);

            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=15&ranking=1&fillIngredients=true&ingredients=" + strippedString;
            System.Diagnostics.Debug.WriteLine(URL);
            Navigation.PushAsync(new SearchByIngredientsListView(URL, 1));
        }
        #endregion
    }
}