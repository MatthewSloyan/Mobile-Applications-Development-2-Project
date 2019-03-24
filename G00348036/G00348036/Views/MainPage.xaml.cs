using G00348036.Views;
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
    }
}
