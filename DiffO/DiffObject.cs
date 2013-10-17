using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffO
{
    public abstract class DiffObject : IDiffObject
    {
        private Dictionary<string, IEnumerable<Difference>> Differences { get; set; }

        protected DiffObject()
        {
            Differences = new Dictionary<string, IEnumerable<Difference>>();
        }

        public void Add<T>(string key, IEnumerable<Difference<T>> difference)
        {
            Differences.Add(key, difference);
        }

        public void Add(string key, IEnumerable<Difference> difference)
        {
            Differences.Add(key, difference);
        }

        public IEnumerable<Difference<T>> Get<T>(string key)
        {
            IEnumerable<Difference> difference;

            if (Differences.TryGetValue(key, out difference))
            {
                return difference as IEnumerable<Difference<T>>;
            }
            return null;
        }

        public Difference<T> CreateDifference<T>(string propName, DifferenceType type, T newValue, T oldValue)
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
