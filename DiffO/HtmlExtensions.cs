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
        public static TKey GetListPropertyDiff<TModel, TKey>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TKey>> expression, DifferenceType type) where TKey : class
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var model = helper.ViewData.Model as IDiffObject;

            if (model != null)
            {
                var differences = model.Get(propertyName) as List<Difference<object>>;

                if (differences != null)
                {
                    switch (type)
                    {
                        case DifferenceType.Removed:
                            return differences.Where(x => x.Type == type).Select(x => x.OldValue) as TKey;
                        case DifferenceType.Added:
                            return differences.Where(x => x.Type == type).Select(x => x.NewValue) as TKey;
                    }
                }
            }
            else
            {
                throw new Exception("Model does not implement IDiffOject");
            }
            return null;
        }

        public static DifferenceType GetPropertyDiffType<TModel, TKey>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TKey>> expression)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var model = helper.ViewData.Model as IDiffObject;

            if (model != null)
            {
                var differences = model.Get(propertyName);
                    
                if (differences != null)
                {
                    var difference = differences.FirstOrDefault() as Difference<object>;

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
