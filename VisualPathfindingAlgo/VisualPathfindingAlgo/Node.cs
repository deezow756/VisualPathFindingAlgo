using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualPathfindingAlgo
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<int> Path;
        public double TotalDistance { get; set; }
        public double Cost { get; set; }
    }
}
