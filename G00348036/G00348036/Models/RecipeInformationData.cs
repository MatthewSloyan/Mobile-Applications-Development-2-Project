using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    class RecipeInformationData
    {
        public bool vegetarian { get; set; }
        public bool vegan { get; set; }
        public bool glutenFree { get; set; }
        public bool dairyFree { get; set; }
        public bool cheap { get; set; }
        public bool veryPopular { get; set; }
        public string servings { get; set; }
        public string readyInMinutes { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string instructions { get; set; }
        public List<ExtendedIngredient> extendedIngredients { get; set; }
    }

    public class ExtendedIngredient
    {
        public string image { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
        public string unit { get; set; }
        public string unitShort { get; set; }
    }
}
