﻿using G00348036.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace G00348036
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            SetUpComponents();
        }
        
        private void SetUpComponents()
        {
            var layout = new StackLayout
            {
                WidthRequest = 100,
                Orientation = StackOrientation.Vertical,
            };

            var image = new Image
            {
                Source = "dfdf",
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var title = new Label
            {
                Text = "Add",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var id = new Label
            {
                Text = "Add",
                IsVisible = false
            };
            
            //create a tap gesture object and attach to stacklayout - collection of tap gestures associated with image
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            tapGestureRecognizer.NumberOfTapsRequired = 1;

            //add to image
            layout.GestureRecognizers.Add(tapGestureRecognizer);
            
            layout.Children.Add(image);
            layout.Children.Add(title);
            layout.Children.Add(id);
            slFavourites.Children.Add(layout);
        }

        #region Event handlers
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           
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
