using GenericCrud.Dto.Base.Interfaces;
using GenericCrud.Filter.Interfaces;

namespace GenericCrud.Result
{
    public class FindResult<TDto>
        where TDto : class, IBaseDTO, new()
    {
        public IEnumerable<TDto> Data { get; }
        public long Count { get; }
        public FindResult(IEnumerable<TDto> data, long count)
        {
            Data = data;
            Count = count;
        }
    }
}
