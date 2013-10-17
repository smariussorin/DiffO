using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffO
{
    public interface IDiffObject
    {
        void Add(string key, IEnumerable<IDifference> difference);

        IEnumerable<IDifference> Get(string key);

        IDifference CreateDifference<T>(string propName, DifferenceType type, T newValue, T oldValue);
    }
}
