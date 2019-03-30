using System.Collections.ObjectModel;

namespace G00348036
{
    class FavouritesViewModel : BaseViewModel
    {
        //global list of recipes
        private ObservableCollection<FavouriteRecipesData> _results;
        public ObservableCollection<FavouriteRecipesData> Results
        {
            get { return _results; }
            set { SetValue(ref _results, value); }
        }

        private FavouriteRecipesData _selectedRecipe;
        public FavouriteRecipesData SelectedRecipe
        {
            get { return _selectedRecipe; }
            set { SetValue(ref _selectedRecipe, value); }
        }

        // Contructor
        public FavouritesViewModel()
        {
            getFavouriteData();
        }

        private void getFavouriteData()
        {

        }
    }
}
