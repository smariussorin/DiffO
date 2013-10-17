using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffO
{
    /// <summary>
    /// Class representing a difference between two property values
    /// </summary>
    public class Difference : IDifference
    {
        public string Prop { get; set; }
        public object ValA { get; set; }
        public object ValB { get; set; }
    }
}
