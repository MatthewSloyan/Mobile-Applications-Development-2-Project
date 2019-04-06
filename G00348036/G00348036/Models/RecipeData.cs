using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace G00348036
{
    public class RecipesData
    {
        public string title { get; set; }
        public string id { get; set; }
        public string image { get; set; }
    }

    public class RandomRecipesData
    {
        public ObservableCollection<RecipesData> recipes { get; set; }
    }
}
