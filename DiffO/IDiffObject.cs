using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffO
{
    public interface IDiffObject
    {
        void Add<T>(string key, IEnumerable<Difference<T>> difference);

        IEnumerable<Difference<T>> Get<T>(string key);

        Difference<T> CreateDifference<T>(string propName, DifferenceType type, T newValue, T oldValue);
    }
}
