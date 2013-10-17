using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffO
{
    public static class BaseExtensions
    {
        public static void CompareTo<T>(this T current, T previous) where T : IDiffObject
        {
            var props = current.GetType().GetProperties();

            foreach (var prop in props)
            {
                var currentValue = prop.GetValue(current);
                var previousValue = prop.GetValue(previous);
                var propName = prop.Name;

                if (prop.PropertyType != typeof(string) && prop.PropertyType.IsClass)
                {
                    var currentEnumerable = currentValue as IEnumerable<object>;

                    if (currentEnumerable == null)
                    {
                        var currentValue2 = currentValue as IDiffObject;

                        if (currentValue2 != null)
                        {
                            var previousValue2 = previousValue as IDiffObject;

                            if (previousValue2 != null)
                            {
                                currentValue2.CompareTo(previousValue2);
                            }
                        }
                    }
                    else
                    {
                        var previousEnumerable = previousValue as IEnumerable<object>;

                        if (previousEnumerable != null)
                        {
                            var currentList = currentEnumerable as List<object> ?? currentEnumerable.ToList();
                            var previousList = previousEnumerable as List<object> ?? previousEnumerable.ToList();

                            var diffList = new List<IDifference>();

                            var diff = current.CreateDifference(propName, DifferenceType.Added, currentList.Except(previousList).ToList(), null);

                            diffList.Add(diff);

                            diff = current.CreateDifference(propName, DifferenceType.Removed, null, previousList.Except(currentList).ToList());

                            diffList.Add(diff);

                            current.Add(propName, diffList);
                        }
                    }
                }
                else
                {
                    var type = DifferenceType.None;

                    if (currentValue == null && previousValue != null)
                    {
                        type = DifferenceType.Removed;
                    }

                    if (currentValue != null && previousValue == null)
                    {
                        type = DifferenceType.Added;
                    }

                    if (currentValue != null && previousValue != null && !currentValue.Equals(previousValue))
                    {
                        type = DifferenceType.Modified;
                    }

                    if (type != DifferenceType.None)
                    {
                        var diff = current.CreateDifference(propName, type, currentValue, previousValue);

                        current.Add(propName, new List<IDifference> { diff });
                    }
                }
            }
        }
    }
}
