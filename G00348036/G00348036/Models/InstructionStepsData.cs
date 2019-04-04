using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    public class InstructionStepsData
    {
        public string name { get; set; }
        public List<Step> steps { get; set; }
    }

    public class Step
    {
        public int number { get; set; }
        public string step { get; set; }
        public List<object> ingredients { get; set; }
        public List<object> equipment { get; set; }
        public Length length { get; set; }
    }

    public class Length
    {
        public int number { get; set; }
        public string unit { get; set; }
    }

    

}
