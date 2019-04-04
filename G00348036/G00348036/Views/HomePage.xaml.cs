using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        //global list of favourite recipes
        private ObservableCollection<SliderRecipesData.RecipesData> ResultsFavourites { get; set; }
        private SliderRecipesData ResultsRandom { get; set; }
        private ObservableCollection<SliderRecipesData.RecipesData> ResultsRandomCon { get; set; }

        public HomePage()
        {
            InitializeComponent();
            // Only load Random recipes once on start up as it slows down the app too much if loading each time the page is loaded
            //SetUpRandom();
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
            ResultsFavourites = Utils.getListFromFile<SliderRecipesData.RecipesData>();
            if (ResultsFavourites == null)
            {
                // Load blank template if there's no favourites added
                LoadSliderInitial();
            }
            else
            {
                LoadSliderInformation(ResultsFavourites, 1);
            }
        }

        private void SetUpRandom()
        {
            string URL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/random?number=15";
            ResultsRandom = Utils.GetSingleApiData<SliderRecipesData>(URL);
            ResultsRandomCon = ResultsRandom.recipes;

            LoadSliderInformation(ResultsRandomCon, 2);
        }

        private void LoadSliderInformation(ObservableCollection<SliderRecipesData.RecipesData> list, int selection)
        {
            foreach (var item in list)
            {
                var layout = new StackLayout
                {
                    WidthRequest = 100,
                    Orientation = StackOrientation.Vertical,
                    StyleId = item.id
                };

                var image = new Image
                {
                    Source = item.image,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                var title = new Label
                {
                    Text = item.title,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                //create a tap gesture object and attach to stacklayout
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;

                layout.Children.Add(image);
                layout.Children.Add(title);
                layout.GestureRecognizers.Add(tapGestureRecognizer);

                if (selection == 1)
                    slFavourites.Children.Add(layout);
                else
                    slRandom.Children.Add(layout);
            }
        }

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
                    HeightRequest = 100,
                    WidthRequest = 100,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                var title = new Label
                {
                    Text = "Favourite",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                layout.Children.Add(image);
                layout.Children.Add(title);
                slFavourites.Children.Add(layout);
            }
        }

        #region Event handlers
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout slSender = (StackLayout)sender;

            Navigation.PushAsync(new RecipeInformation(slSender.StyleId));
        }

        private void BtnSearchByIngredients_Clicked(object sender, EventArgs e)
        {
            //go to SearchByIngredients.xaml
            Navigation.PushAsync(new SearchByIngredients());
        }

        private void BtnSearchByRecipe_Clicked(object sender, EventArgs e)
        {
            //go to SearchByRecipes.xaml
            Navigation.PushAsync(new SearchByRecipe());
        }

        private void BtnSearchByImage_Clicked(object sender, EventArgs e)
        {
            //go to SearchByImage.xaml
            Navigation.PushAsync(new SearchByImage());
        }

        private void BtnFavourites_Clicked(object sender, EventArgs e)
        {
            //go to Favourites.xaml
            Navigation.PushAsync(new Favourites());
        }
        #endregion
    }
}