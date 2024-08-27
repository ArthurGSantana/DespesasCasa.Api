using AutoMapper;
using DespesasCasa.Domain.Entity;
using DespesasCasa.Domain.Interface.Repository;
using DespesasCasa.Domain.Interface.Service;
using DespesasCasa.Domain.Model.Dto;

namespace DespesasCasa.Application.Service;

public class CollectionService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICollectionService
{
    public async Task<CollectionDto> GetCollectionAsync(Guid id)
    {
        var collection = await _unitOfWork.CollectionRepository.GetAsync(false, x => x.Id == id);

        if (collection == null)
        {
            throw new Exception("Collection not found");
        }

        return _mapper.Map<CollectionDto>(collection);
    }

    public async Task<List<CollectionDto>> GetAllAsync()
    {
        return _mapper.Map<List<CollectionDto>>(await _unitOfWork.CollectionRepository.GetAllAsync());
    }

    public async Task<Guid> CreateAsync(CollectionDto collection)
    {
        var existing = await _unitOfWork.CollectionRepository.FindAsync(x => x.Id == collection.Id);

        if (existing)
        {
            throw new Exception("Collection already exists");
        }

        var newCollection = _mapper.Map<Collection>(collection);

        await _unitOfWork.CollectionRepository.AddAsync(newCollection);
        await _unitOfWork.CommitAsync();

        return newCollection.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var collection = await _unitOfWork.CollectionRepository.GetAsync(false, x => x.Id == id);

        if (collection is null)
        {
            throw new Exception("Collection not found");
        }

        _unitOfWork.CollectionRepository.Remove(collection);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
