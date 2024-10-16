
using GenericCrud.Dto.Base.Interfaces;
using GenericCrud.Filter.Interfaces;
using GenericCrud.Filter.Sort;
using GenericCrud.Result;

namespace GenericCrud.Services.Interfaces.Base
{
    public interface IService<TDto, TFilter>
        where TDto : class, IBaseDTO, new()
        where TFilter : class, IDtoFilter, new()
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        FindResult<TDto> Find(TFilter filter, CountType countType);
        Task<TDto> GetByIdAsync(int id);
        Task AddAsync(TDto dto);
        Task UpdateAsync(int id, TDto dto);
        Task DeleteAsync(int id);
    }
}
