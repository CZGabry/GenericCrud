using AutoMapper;
using GenericCrud.Dto.Base.Interfaces;
using GenericCrud.Filter;
using GenericCrud.Filter.Base;
using GenericCrud.Filter.Interfaces;
using GenericCrud.Filter.Sort;
using GenericCrud.Repositories.Interfaces.Base;
using GenericCrud.Result;
using GenericCrud.Services.Interfaces.Base;
using System.Linq.Expressions;

namespace GenericCrud.Services.Base
{
    public class Service<TDto, TFilter, TEntity> : IService<TDto, TFilter>
    where TDto : class, IBaseDTO, new()
    where TFilter : class, IDtoFilter, new()
    where TEntity : class, new()
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public Service(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<TDto>>(await _repository.GetAllAsync());

        public async Task<TDto> GetByIdAsync(int id) =>
        _mapper.Map<TDto>(await _repository.GetByIdAsync(id));
        protected FindResult<TDto> BaseFind(TFilter filter, CountType countType, params Expression<Func<TEntity, object>>[] additionalNavigationProperties)
        {
            var predicate = GetPredicate(filter);
            var sortInfo = filter.ToSortInfo<TEntity, TDto>();

            // Combine navigation properties from IncludeProperties and any passed as arguments
            var navigationProperties = GetNavigationPropertiesFromStrings(filter.IncludeProperties)
                .Concat(additionalNavigationProperties)
                .ToArray();

            FindResponse<TEntity> results = _repository.Find(predicate, countType, sortInfo, filter.PageSize, filter.GetOffset(), navigationProperties);
            IEnumerable<TDto> dtoResults = _mapper.Map<IEnumerable<TDto>>(results.Data);

            return new FindResult<TDto>(dtoResults, results.Count);
        }

        private IEnumerable<Expression<Func<TEntity, object>>> GetNavigationPropertiesFromStrings(string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                var parameter = Expression.Parameter(typeof(TEntity), "entity");
                var property = Expression.Property(parameter, propertyName);
                yield return Expression.Lambda<Func<TEntity, object>>(property, parameter);
            }
        }



        public FindResult<TDto> Find(TFilter filter, CountType countType)
        {
            return BaseFind(filter, countType);
        }

        public virtual Expression<Func<TEntity, bool>> GetPredicate(TFilter filter)
        {
            return filter.ToExpression<TEntity>();
        }

        public async Task AddAsync(TDto dto) =>
            await _repository.AddAsync(_mapper.Map<TEntity>(dto));

        public async Task UpdateAsync(int id, TDto dto) =>
            await _repository.UpdateAsync(id, _mapper.Map<TEntity>(dto));

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
