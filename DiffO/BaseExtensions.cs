using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiffO
{
    public static class BaseExtensions
    {
        public static void CompareTo<T>(this T current, T previous, params string[] ignoreProps) where T : IDiffObject
        {
            var props = current.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                  p => p.CanRead && !ignoreProps.Contains(p.Name));

            foreach (var prop in props)
            {
                var currentValue = prop.GetValue(current);
                var previousValue = prop.GetValue(previous);
                var propName = prop.Name;

                if (CanDirectlyCompare(prop.PropertyType))
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

                        current.Add(propName, new List<Difference<object>> { diff });
                    }
                }
                else if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                {
                    var currentEnumerable = currentValue as IEnumerable<object>;
                    var previousEnumerable = previousValue as IEnumerable<object>;

                    var currentList = currentEnumerable as List<object> ?? currentEnumerable.ToList();
                    var previousList = previousEnumerable as List<object> ?? previousEnumerable.ToList();

                    var diffList = new List<Difference<List<object>>>();

                    var diff = current.CreateDifference(propName, DifferenceType.Added, currentList.Except(previousList).ToList(), null);

                    diffList.Add(diff);

                    diff = current.CreateDifference(propName, DifferenceType.Removed, null, previousList.Except(currentList).ToList());

                    diffList.Add(diff);

                    current.Add(propName, diffList);    
                }
                else
                {
                    var currentValue2 = currentValue as IDiffObject;

                    if (currentValue2 != null)
                    {
                        var previousValue2 = previousValue as IDiffObject;

                        if (previousValue2 != null)
                        {
                            currentValue2.CompareTo(previousValue2, ignoreProps);
                        }
                    }
                }
            }
        }

        private static bool CanDirectlyCompare(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }
    }
}
