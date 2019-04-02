using System;
using System.Collections.Generic;
using System.Text;

namespace G00348036
{
    class InstructionStepsData
    {
        public string name { get; set; }
        public List<Steps> steps { get; set; }
    }

    public class Steps
    {
        public string number { get; set; }
        public string step { get; set; }
    }
}
