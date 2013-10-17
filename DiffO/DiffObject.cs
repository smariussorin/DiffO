using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffO
{
    public abstract class DiffObject : IDiffObject
    {
        private Dictionary<string, IDifference> Differences { get; set; }

        public DiffObject()
        {
            Differences = new Dictionary<string, IDifference>();
        }
        public void Add(string key, IDifference difference)
        {
            Differences.Add(key, difference);
        }

        public IDifference Get(string key)
        {
            IDifference difference;
            if (Differences.TryGetValue(key, out difference))
            {
                return difference;
            }
            else
            {
                throw new ArgumentException("key does not implement DiffObject or was not found in the dictionary", "key");
            }
        }
    }
}
