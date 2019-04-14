using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace G00348036
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchByIngredientsListView : ContentPage
	{
        public SearchByIngredientsListView(string URL, int selection)
        {
            InitializeComponent ();

            // Tried to implement a loading activity but it doesn't seem to before the page loads
            activityInd.IsEnabled = true;
            this.BindingContext = new RecipesViewModel(new PageService(), URL, selection);
            activityInd.IsEnabled = false;
        }

        #region Event Handlers
        // When a list item is selected get the object of the recipe and send it's id to the RecipeInformation page to load full recipe
        private void LvRecipes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // From testing I found that on pressing the back button the selected value would be null which would cause the app to crash
            // To fix this I have implemented a return if null which seems to work perfectly.
            if (e.SelectedItem == null)
            {
                return;
            }

            RecipeResults i = e.SelectedItem as RecipeResults;
            (BindingContext as RecipesViewModel).NavigatePage(i.id);
        }

        // When button is clicked get object from list and pass to view model
        private void BtnAddToFavourites_Clicked(object sender, System.EventArgs e)
        {
            RecipeResults s = (sender as Button).CommandParameter as RecipeResults;

            (BindingContext as RecipesViewModel).AddToFavourites(s);

            DisplayAlert("Success", "Recipe has been added to favourites.", "OK");
        }

        // I wanted to implement a quick way to get through the list, so swiping will remove the recipe from the list
        // but from testing it's not the most intutitive feature but it still works.
        // Also it doesn't seem to sometimes get the right object, as it could be due to the selected recipe passed in
        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            // Get the object that was swiped and remove from list
            RecipeResults s = e.Parameter as RecipeResults;
            (BindingContext as RecipesViewModel).RemoveFromList(s);
        }
        #endregion
    }
}