using AutoMapper;
using GenericCrud.Repositories.Interfaces.Base;
using GenericCrud.Services.Interfaces.Base;

namespace GenericCrud.Services.Base
{
    public class Service<TDto, TEntity> : IService<TDto>
    where TDto : class
    where TEntity : class
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

        public async Task AddAsync(TDto dto) =>
            await _repository.AddAsync(_mapper.Map<TEntity>(dto));

        public async Task UpdateAsync(int id, TDto dto) =>
            await _repository.UpdateAsync(id, _mapper.Map<TEntity>(dto));

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
