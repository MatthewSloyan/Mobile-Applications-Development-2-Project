// Name: Matthew Sloyan
// ID: G00348036
// https://github.com/MatthewSloyan/Mobile-Applications-Development-2-Project

using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        // Global list of favourite recipes
        // Had to use an ObservableCollection as method in utils class uses them
        private ObservableCollection<RecipesData> ResultsFavourites { get; set; }
        private ObservableCollection<RecipesData> ResultsRandomCon { get; set; }

        public HomePage()
        {
            InitializeComponent();
            // Only load Random recipes once on start up as it slows down the app too much if loading each time the page is loaded
            SetUpRandom();
            OnAppearing();
        }

        // Calls when ever the page comes into view, this will update the favourites and random section when new data is recieved
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetUpFavourites();
        }

        private void SetUpFavourites()
        {
            //Clear all children from favourites section so it doesn't add more when reloaded
            slFavourites.Children.Clear();

            // Load favourites from file and add at run time
            ResultsFavourites = Utils.getListFromFile<RecipesData>();
            if (ResultsFavourites == null)
            {
                // Load blank template if there's no favourites added
                LoadSliderInitial();
            }
            else
            {
                // Or load actual saved favourites from list
                LoadSliderInformation(ResultsFavourites, 1);
            }
        }

        // Get random recipes from api and load using the same method as favourites for code reuse
        private void SetUpRandom()
        {
            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/random?number=12";

            // I tried to implement a background task here to speed up initial app load, but I couldn't get it work as it would crash.
            try
            {
                // Get random results and convert to same type as favoutites and the json is different
                RandomRecipesData ResultsRandom = Utils.GetSingleApiData<RandomRecipesData>(URL);
                ResultsRandomCon = ResultsRandom.recipes;
                LoadSliderInformation(ResultsRandomCon, 2);
            }
            catch (Exception)
            {
                DisplayAlert("Error", "Random Recipes can not be loaded.", "OK");
            }
        }

        private void LoadSliderInformation(ObservableCollection<RecipesData> list, int selection)
        {
            // For each recipe in the list create a stacklayout, image, and label at run time and add to horizontal scroll bar
            foreach (var item in list)
            {
                var layout = new StackLayout
                {
                    WidthRequest = 100,
                    Orientation = StackOrientation.Vertical,
                    StyleId = item.id
                };

                var image = new CircleImage
                {
                    Source = item.image,
                    HeightRequest = 70,
                    WidthRequest = 70,
                    Margin = new Thickness(0, 10, 0, 0),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Aspect = Aspect.AspectFill
                };

                var title = new Label
                {
                    Text = item.title,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                //create a tap gesture object and attach to stacklayout
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;

                layout.Children.Add(image);
                layout.Children.Add(title);
                layout.GestureRecognizers.Add(tapGestureRecognizer);

                // Selection to determine where the method was called from and where to add
                if (selection == 1)
                    slFavourites.Children.Add(layout);
                else
                    slRandom.Children.Add(layout);
            }
        }

        // Create a default display if there's no favourites saved, for design purposes
        private void LoadSliderInitial()
        {
            for (int i = 0; i < 5; i++)
            {
                var layout = new StackLayout
                {
                    WidthRequest = 100,
                    Orientation = StackOrientation.Vertical
                };

                var image = new Image
                {
                    Source = "recipeIcon.png",
                    HeightRequest = 70,
                    WidthRequest = 70,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Aspect = Aspect.AspectFill
                };

                var title = new Label
                {
                    Text = "Add Your Favourites",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                layout.Children.Add(image);
                layout.Children.Add(title);
                slFavourites.Children.Add(layout);
            }
        }

        #region Event handlers
        // If a recipe is selected in either horizontal scroll bars, get the stacklayout and send it's styleId which is the recipe id
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout slSender = (StackLayout)sender;

            Navigation.PushAsync(new RecipeInformation(slSender.StyleId));
        }

        private void BtnSearchByIngredients_Clicked(object sender, EventArgs e)
        {
            // Gets the MainPage, and set it's current page to each option
            var parentPage = Parent as TabbedPage;
            parentPage.CurrentPage = parentPage.Children[1];
        }

        private void BtnSearchByRecipe_Clicked(object sender, EventArgs e)
        {
            var parentPage = Parent as TabbedPage;
            parentPage.CurrentPage = parentPage.Children[2];
        }

        private void BtnSearchByImage_Clicked(object sender, EventArgs e)
        {
            var parentPage = Parent as TabbedPage;
            parentPage.CurrentPage = parentPage.Children[3];
        }

        private void BtnFavourites_Clicked(object sender, EventArgs e)
        {
            var parentPage = Parent as TabbedPage;
            parentPage.CurrentPage = parentPage.Children[4];
        }
        #endregion
    }
}