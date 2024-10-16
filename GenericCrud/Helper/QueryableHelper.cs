using GenericCrud.Filter.Sort;
using System.Linq.Expressions;
using System.Reflection;

namespace GenericCrud.Helper
{
    public static class QueryableHelper
    {
        public static IQueryable<T> UpdateQueryWithOrderBy<T>(IQueryable<T> source, SortInfo<T> ordering) where T : class
        {
            Expression queryExpr = source.Expression;
            var parameter = Expression.Parameter(source.ElementType, "p");
            string methodAsc = "OrderBy";
            string methodDesc = "OrderByDescending";

            foreach (var o in ordering)
            {
                Type elementType = source.ElementType;
                Expression propertiesExpression = parameter;

                foreach (var propertyName in o.Key.Split('.'))
                {
                    PropertyInfo property = elementType.GetProperty(propertyName);
                    if (property == null) throw new ArgumentException($"Sorter: column name '{o.Key}' not found.");
                    propertiesExpression = Expression.MakeMemberAccess(propertiesExpression, property);

                    elementType = property.PropertyType;
                }

                // Call the LINQ orderBy specific method, passing an expression with the current parameters.
                queryExpr = Expression.Call(typeof(Queryable), o.Value ? methodAsc : methodDesc,
                    new Type[] { source.ElementType, elementType },
                    queryExpr, Expression.Quote(Expression.Lambda(propertiesExpression, parameter)));
                methodAsc = "ThenBy";
                methodDesc = "ThenByDescending";
            }

            return source.Provider.CreateQuery<T>(queryExpr);
        }

        public static IQueryable<T> UpdateQueryWithPagination<T>(IQueryable<T> dbQuery, int limit, int offset)
            where T : class
        {
            if (offset > 0) dbQuery = dbQuery.Skip(offset);
            if (limit > 0) dbQuery = dbQuery.Take(limit);

            return dbQuery;
        }
    }
}
