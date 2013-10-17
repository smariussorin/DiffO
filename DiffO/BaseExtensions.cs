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
                    var currentList = currentValue as IEnumerable<object>;

                    if (currentList == null)
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
                        var previousList = previousValue as IEnumerable<object>;

                        if (previousList != null)
                        {
                            var v = new Difference
                            {
                                Prop = propName,
                                ValA = currentList.Except(previousList).ToList(),
                                ValB = previousList.Except(currentList).ToList()
                            };

                            current.Add(propName, v);
                        }
                    }
                }
                else
                {
                    var v = new Difference
                    {
                        Prop = propName,
                        ValA = currentValue,
                        ValB = previousValue
                    };

                    if ((v.ValA == null && v.ValB != null) ||
                        (v.ValA != null && v.ValB == null) ||
                        (v.ValA != null && v.ValB != null && !v.ValA.Equals(v.ValB)))
                    {
                        current.Add(propName, v);
                    }
                }
            }
        }
    }
}
