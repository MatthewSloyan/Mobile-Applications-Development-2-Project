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
        public string number { get; set; }
        public string step { get; set; }
    }
}
