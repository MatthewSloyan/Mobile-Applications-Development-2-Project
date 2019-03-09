using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    public class SearchByIngredientsData
    {
        public string id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string usedIngredientCount { get; set; }
        public string missedIngredientCount { get; set; }
        public string likes { get; set; }
    }

    public class RootObject
    {
        public List<SearchByIngredientsData> results { get; set; }
    }
}
