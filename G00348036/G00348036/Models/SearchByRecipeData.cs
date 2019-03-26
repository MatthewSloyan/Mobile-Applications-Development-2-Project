using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    public class SearchByRecipeData
    {
        public List<Result> results { get; set; }
        public string baseUri { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
        public int processingTimeMs { get; set; }
        public long expires { get; set; }
        public bool isStale { get; set; }

        public class Result
        {
            public int id { get; set; }
            public string title { get; set; }
            public int readyInMinutes { get; set; }
            public int servings { get; set; }
            public string image { get; set; } 
            public List<string> imageUrls { get; set; }
        }
    }
    
    
}
