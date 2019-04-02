using G00348036.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace G00348036
{
    public partial class MainPage : ContentPage
    {
        //global list of favourite recipes
        private ObservableCollection<FavouriteRecipesData> Results { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        // Calls when ever the page comes into view, this will update the favourites and random section when new data is recieved
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetUpComponents();
        }

        private void SetUpComponents()
        {
            Results = Utils.getListFromFile<FavouriteRecipesData>();

            //Clear all children from favourites section so it doesn't add more when reloaded
            slFavourites.Children.Clear();

            if (Results == null)
                return;

            foreach (var item in Results)
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

                var id = new Label
                {
                    Text = item.id,
                    IsVisible = false
                };

                //create a tap gesture object and attach to stacklayout
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                tapGestureRecognizer.NumberOfTapsRequired = 1;

                //add to image
                layout.GestureRecognizers.Add(tapGestureRecognizer);

                layout.Children.Add(image);
                layout.Children.Add(title);
                layout.Children.Add(id);
                slFavourites.Children.Add(layout);

                string url = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/random?number=10";
            }
        }

        #region Event handlers
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout slSender = (StackLayout)sender;

            Navigation.PushAsync(new RecipeInformation(slSender.StyleId));
        }
        #endregion
        
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
    }
}
