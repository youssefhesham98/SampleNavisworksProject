using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleNavisworksProject
{
    public class ClashTestData
    {
        public string TestName { get; set; }
        public List<ClashGroupData> Groups { get; set; }
    }

    public class ClashGroupData
    {
        public string GroupName { get; set; }
        public List<ClashData> Clashes { get; set; }
    }
    public class ClashData
    {
        public string ClashName { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Status { get; set; }
        public double Distance { get; set; }
    }
}
