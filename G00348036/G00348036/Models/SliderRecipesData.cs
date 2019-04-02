using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace G00348036
{
    class SliderRecipesData
    {
        public ObservableCollection<RecipesData> recipes { get; set; }

        public class RecipesData
        {
            public string title { get; set; }
            public string id { get; set; }
            public string image { get; set; }
        }
    }

}
