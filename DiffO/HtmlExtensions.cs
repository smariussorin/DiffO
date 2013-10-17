using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DiffO
{
    public static class HtmlExtensions
    {
        public static List<object> GetListPropertyDiff<TModel, TKey>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TKey>> expression, DifferenceType type) where TKey : class 
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var model = helper.ViewData.Model as IDiffObject;

            if (model != null)
            {
                var differences = model.Get<List<object>>(propertyName);

                if (differences != null)
                {
                    switch (type)
                    {
                        case DifferenceType.Removed:
                            return differences.Where(x => x.Type == type).Select(x => x.OldValue).FirstOrDefault();
                        case DifferenceType.Added:
                            return differences.Where(x => x.Type == type).Select(x => x.NewValue).FirstOrDefault();
                    }
                }
                return null;
            }
            throw new Exception("Model does not implement IDiffOject");
        }

        public static DifferenceType GetPropertyDiffType<TModel, TKey>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TKey>> expression)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var model = helper.ViewData.Model as IDiffObject;

            if (model != null)
            {
                var differences = model.Get<object>(propertyName);
                    
                if (differences != null)
                {
                    var difference = differences.FirstOrDefault();

                    if (difference != null)
                    {
                        return difference.Type;
                    }
                }
            }
            else
            {
                throw new Exception("Model does not implement IDiffOject");
            }
            return DifferenceType.None;
        }

        public static string GetPropertyDiffColor<TModel, TKey>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TKey>> expression)
        {
            switch (GetPropertyDiffType(helper, expression))
            {
                case DifferenceType.Removed:
                    return "red";
                case DifferenceType.Modified:
                    return "yellow";
                case DifferenceType.Added:
                    return "green";
                default:
                    return "";
            }
        }
    }
}
