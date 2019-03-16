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

        //private string id;
        //public string Id
        //{
        //    get { return id; }
        //    set { id = value; }
        //}

        //private string title;
        //public string Title
        //{
        //    get { return title; }
        //    set { title = value; }
        //}

        //private string usedIngredientCount;
        //public string UsedIngredientCount
        //{
        //    get { return usedIngredientCount; }
        //    set { usedIngredientCount = value; }
        //}

        //private string missedIngredientCount;
        //public string MissedIngredientCount
        //{
        //    get { return missedIngredientCount; }
        //    set { missedIngredientCount = value; }
        //}

        //private string likes;
        //public string Likes
        //{
        //    get { return likes; }
        //    set { likes = value; }
        //}
    }

    public class RootObject
    {
        public List<SearchByIngredientsData> Results { get; set; }
    }
}
