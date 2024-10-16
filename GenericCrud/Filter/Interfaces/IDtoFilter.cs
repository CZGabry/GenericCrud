using GenericCrud.Dto.Base.Interfaces;
using GenericCrud.Filter.Sort;
using System.Linq.Expressions;

namespace GenericCrud.Filter.Interfaces
{
    public interface IDtoFilter
    {
        int CurPage { get; set; }
        int PageSize { get; set; }
        Dictionary<string, OrderValues> FieldsOrder { get; set; }

        string[] IncludeProperties { get; set; }

        Expression<Func<TOut, bool>> ToExpression<TOut>() where TOut : class, new();
        SortInfo<TOut> ToSortInfo<TOut, TDtoEntity>() where TOut : class where TDtoEntity : class, IBaseDTO;
        int GetOffset();
    }

    public enum OrderValues { ASC = 1, DESC = 2 }
}
