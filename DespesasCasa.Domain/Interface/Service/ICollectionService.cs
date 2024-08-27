using DespesasCasa.Domain.Model.Dto;

namespace DespesasCasa.Domain.Interface.Service;

public interface ICollectionService
{
    Task<CollectionDto> GetCollectionAsync(Guid id);
    Task<List<CollectionDto>> GetAllAsync();
    Task<Guid> CreateAsync(CollectionDto collection);
    Task<bool> DeleteAsync(Guid id);
}
