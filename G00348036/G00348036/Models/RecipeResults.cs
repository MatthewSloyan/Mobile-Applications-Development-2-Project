using System.Collections.ObjectModel;

namespace G00348036
{
    public class RecipeResults
    {
        public string id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public int readyInMinutes { get; set; }
        public int servings { get; set; }
        public string usedIngredientCount { get; set; }
        public string missedIngredientCount { get; set; }
    }

    public class SearchRecipesData
    {
        public ObservableCollection<RecipeResults> results { get; set; }
        public string baseUri { get; set; }
    }
}
