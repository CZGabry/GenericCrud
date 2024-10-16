using System.Linq.Expressions;

namespace GenericCrud.Filter.Sort
{
    public class SortInfo<TModel> : Dictionary<string, bool>
        where TModel : class
    {
        public SortInfo<TModel> AddSortAsc<TKey>(Expression<Func<TModel, TKey>> sortExpression)
        {
            string fieldName = (sortExpression.Body as MemberExpression).Member.Name;
            Add(fieldName, true);

            return this;
        }

        public SortInfo<TModel> AddSortDesc<TKey>(Expression<Func<TModel, TKey>> sortExpression)
        {
            string fieldName = (sortExpression.Body as MemberExpression).Member.Name;
            Add(fieldName, false);

            return this;
        }
        public SortInfo<TModel> AddSortForSubmemberAsc<TKey>(Expression<Func<TModel, TKey>> sortExpression)
        {
            string path = GetPropertyPath(sortExpression);
            this.Add(path, true);
            return this;
        }

        public SortInfo<TModel> AddSortForSubmemberDesc<TKey>(Expression<Func<TModel, TKey>> sortExpression)
        {
            string path = GetPropertyPath(sortExpression);
            this.Add(path, false);
            return this;
        }

        private static string GetPropertyPath(Expression expression)
        {
            if (expression is LambdaExpression lambda)
            {
                return GetPropertyPath(lambda.Body);
            }
            else if (expression is MemberExpression member)
            {
                var prevPath = GetPropertyPath(member.Expression);
                return string.IsNullOrEmpty(prevPath) ? member.Member.Name : $"{prevPath}.{member.Member.Name}";
            }
            else if (expression is UnaryExpression unary)
            {
                return GetPropertyPath(unary.Operand);
            }
            return string.Empty;
        }
    }

    public enum CountType
    {
        No = 0,
        Yes = 1,
        Estimate = 2
    }
}
