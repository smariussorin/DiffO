======
DiffO library is specifically designed for ASP.NET MVC, containing a set of Html helpers for marking the differences in the view.

Its usage is very simple. All you have to do, is to inherit your models from <code> DiffObject</code> and call the <code>CompareTo()</code> method. Calling the method, will iterate through the objects properties and if it finds properties that also inherit <code>DiffObject</code> compares them as well.

```
void CompareTo<T>(this T current, T previous) where T : IDiffObject
```

##Html helpers:

```
List<object> GetListPropertyDiff<TModel, TKey>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TKey>> expression, DifferenceType type)
```

>Use for list property. The <code>type</code> parameter specifies whether to get the added or removed items.

Example:

```
var removed = Html.GetListPropertyDiff(x => x.Items, DifferenceType.Removed)
```
```
 var added = Html.GetListPropertyDiff(x => x.Items, DifferenceType.Added)
```

---------------------------------------


```
string GetPropertyDiffStyle<TModel, TKey>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TKey>> expression);
```

> Based on the difference type (modified, removed, added) calling this method will return a specific style

Example:

```
<li style="@Html.GetPropertyDiffStyle(x => x.Name)">@Model.Name</li>
```

---------------------------------------


```
object GetPropertyDiffOldValue<TModel, TKey>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TKey>> expression);
```

>If a certain property's value has been removed, call this method to get the old value.

Example:

```
<li style="@Html.GetPropertyDiffStyle(x => x.Name)">@(Model.Color ?? Html.GetPropertyDiffOldValue(x => x.Color))</li>
```

---------------------------------------

```
DifferenceType GetPropertyDiffType<TModel, TKey>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TKey>> expression);
```

> Get the type of modification this property suffered, to create a custom style.


