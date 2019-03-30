using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    class SearchByImageApiData
    {
        public List<Respons> responses { get; set; }
    }

    public class Respons
    {
        public List<LocalizedObjectAnnotation> localizedObjectAnnotations { get; set; }
    }

    public class LocalizedObjectAnnotation
    {
        public string mid { get; set; }
        public string name { get; set; }
        public double score { get; set; }
    }
}
