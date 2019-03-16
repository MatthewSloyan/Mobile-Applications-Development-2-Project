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
        public bool veryHealthy { get; set; }
        public bool cheap { get; set; }
        public bool veryPopular { get; set; }
       // public bool sustainable { get; set; }
        //public string weightWatcherSmartPoints { get; set; }
        //public string gaps { get; set; }
        //public bool lowFodmap { get; set; }
        //public bool ketogenic { get; set; }
        //public bool whole30 { get; set; }
        public string servings { get; set; }
        //public string sourceUrl { get; set; }
        //public string spoonacularSourceUrl { get; set; }
        //public string aggregateLikes { get; set; }
        //public string creditText { get; set; }
        //public string sourceName { get; set; }
        public List<ExtendedIngredient> extendedIngredients { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string readyInMinutes { get; set; }
        public string image { get; set; }
        //public string imageType { get; set; }
        public string instructions { get; set; }
    }

    public class ExtendedIngredient
    {
        //public string id { get; set; }
        //public string aisle { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
        public string unit { get; set; }
        public string unitShort { get; set; }
        //public string unitLong { get; set; }
        //public string originalString { get; set; }
    }
}
