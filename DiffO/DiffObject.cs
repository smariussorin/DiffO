using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffO
{
    public abstract class DiffObject : IDiffObject
    {
        private Dictionary<string, IEnumerable<IDifference>> Differences { get; set; }

        protected DiffObject()
        {
            Differences = new Dictionary<string, IEnumerable<IDifference>>();
        }

        public void Add(string key, IEnumerable<IDifference> difference)
        {
            Differences.Add(key, difference);
        }

        public IEnumerable<IDifference> Get(string key)
        {
            IEnumerable<IDifference> difference;

            if (Differences.TryGetValue(key, out difference))
            {
                return difference;
            }
            return null;
        }

        public IDifference CreateDifference<T>(string propName, DifferenceType type, T newValue, T oldValue)
        {
            return new Difference<T>
                       {
                           NewValue = newValue,
                           OldValue = oldValue,
                           Type = type,
                           Prop = propName
                       };

        }
    }
}
