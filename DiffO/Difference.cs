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
    public class Difference<T> : IDifference
    {
        public string Prop { get; set; }
        public DifferenceType Type { get; set; }
        public T NewValue { get; set; }
        public T OldValue { get; set; }
    }
}
