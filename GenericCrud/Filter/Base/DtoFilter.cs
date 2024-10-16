using GenericCrud.Dto.Base.Interfaces;
using GenericCrud.Filter.Interfaces;
using GenericCrud.Filter.Sort;
using System.Linq.Expressions;

namespace GenericCrud.Filter.Base
{
    public class ExcludeFromFilterAttribute : Attribute { }
    public interface ISnakeCaseConvertible
    {
    }
    public class DtoFilter : IDtoFilter, ISnakeCaseConvertible
    {
        [ExcludeFromFilter] public string? RicercaLibera { get; set; }

        [ExcludeFromFilter] public int CurPage { get; set; }

        [ExcludeFromFilter] public int PageSize { get; set; }

        [ExcludeFromFilter] public Dictionary<string, OrderValues> FieldsOrder { get; set; }

        [ExcludeFromFilter] public string[] IncludeProperties { get; set; }

        public DtoFilter()
        {
            FieldsOrder = new Dictionary<string, OrderValues>();

            CurPage = 1;
            PageSize = 10;

            IncludeProperties = Array.Empty<string>();
        }

        public int GetOffset()
        {
            return (CurPage - 1) * PageSize;
        }

        public virtual Expression<Func<TOut, bool>> ToExpression<TOut>() where TOut : class, new()
        {
            var param = Expression.Parameter(typeof(TOut), "x");
            Expression combined = null;

            foreach (var property in GetType().GetProperties().Where(p => !Attribute.IsDefined(p, typeof(ExcludeFromFilterAttribute))))
            {
                var propertyValue = property.GetValue(this);
                // Check if property value is not null and is of a type that should be included in the filter
                if (propertyValue != null && (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string)))
                {
                    var propAccess = Expression.Property(param, property.Name);
                    var valueAccess = Expression.Constant(propertyValue);
                    var equality = Expression.Equal(propAccess, valueAccess);

                    combined = combined == null ? equality : Expression.AndAlso(combined, equality);
                }
            }

            // If combined is still null (no properties had non-null values), return an expression that always evaluates to true
            if (combined == null)
            {
                combined = Expression.Constant(true); // Fallback to a default true if no properties to filter
            }

            return Expression.Lambda<Func<TOut, bool>>(combined, param);
        }



        public virtual SortInfo<TOut> ToSortInfo<TOut, TDtoEntity>()
            where TOut : class where TDtoEntity : class, IBaseDTO
        {
            SortInfo<TOut> sortInfo = new SortInfo<TOut>();
            return sortInfo;
        }

        public virtual object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

        public (Expression, Expression) FixNullableParameters(Expression e1, Expression e2)
        {
            // Convert the non-nullable type to nullable.
            if (IsNullableType(e1.Type) && !IsNullableType(e2.Type))
                e2 = Expression.Convert(e2, e1.Type);
            else if (!IsNullableType(e1.Type) && IsNullableType(e2.Type))
                e1 = Expression.Convert(e1, e2.Type);

            return (e1, e2);
        }

        public bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
