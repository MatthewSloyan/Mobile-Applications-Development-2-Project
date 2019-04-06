
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

            this.BindingContext = new RecipesViewModel(new PageService(), URL, selection);
        }

        private void LvRecipes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            RecipeResults selected = e.SelectedItem as RecipeResults;
            (BindingContext as RecipesViewModel).NavigatePage(selected.id);
        }

        private void BtnAddToFavourites_Clicked(object sender, System.EventArgs e)
        {
            RecipeResults s = (sender as Button).CommandParameter as RecipeResults;
            (BindingContext as RecipesViewModel).AddToFavourites(s);
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            // Get the object that was swiped and remove from list
            RecipeResults s = e.Parameter as RecipeResults;
            (BindingContext as RecipesViewModel).RemoveFromList(s);
        }
    }
}