using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G00348036
{
    class FavouritesViewModel : BaseViewModel
    {
        //global list of recipes which use iNotifyPropertyChanged implemented through the BaseViewModel
        private ObservableCollection<RecipesData> _results;
        public ObservableCollection<RecipesData> Results
        {
            get { return _results; }
            set { SetValue(ref _results, value); }
        }

        private RecipesData _selectedRecipe;
        public RecipesData SelectedRecipe
        {
            get { return _selectedRecipe; }
            set { SetValue(ref _selectedRecipe, value); }
        }

        // Page service interface, set up in contructor
        private readonly IPageService _pageService;

        // Contructor
        public FavouritesViewModel(PageService pageService)
        {
            _pageService = pageService;
            Results = Utils.getListFromFile<RecipesData>();
        }

        // Remove from list in view and also remove from file
        public void RemoveFromFavourites(RecipesData f)
        {
            Results.Remove(f);
            SelectedRecipe = null;

            Utils.RemoveFavouriteFromFile(f);
        }

        // Navigate to recipe page if recipe is selected using pageService
        public void NavigatePage(string id)
        {
            _pageService.PushAsync(new RecipeInformation(id));
        }
    }
}
