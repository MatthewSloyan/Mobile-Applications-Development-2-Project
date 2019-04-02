using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    public class SearchByRecipeData
    {
        public List<Result> results { get; set; }
        public string baseUri { get; set; }

        public class Result
        {
            public string id { get; set; }
            public string title { get; set; }
            public string image { get; set; }
            public int readyInMinutes { get; set; }
            public int servings { get; set; }
        }
    }
}
