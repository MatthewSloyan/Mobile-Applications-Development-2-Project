
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByRecipesListView : ContentPage
    {
        public SearchByRecipesListView(string URL, int selection)
        {
            InitializeComponent();

            this.BindingContext = new RecipesViewModel(URL, selection);
        }

        private void LvRecipes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SearchByRecipeData.Result selected = e.SelectedItem as SearchByRecipeData.Result;

            Navigation.PushAsync(new RecipeInformation(selected.id.ToString()));
        }

        private void BtnAddToFavourites_Clicked(object sender, System.EventArgs e)
        {
            SearchByRecipeData.Result s = (sender as Button).CommandParameter as SearchByRecipeData.Result;

            (BindingContext as RecipesViewModel).AddToFavouritesR(s);
        }
    }
}