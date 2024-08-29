using AutoMapper;
using DespesasCasa.Application.Security;
using DespesasCasa.Domain.Entity;
using DespesasCasa.Domain.Enum;
using DespesasCasa.Domain.Exceptions;
using DespesasCasa.Domain.Interface.Repository;
using DespesasCasa.Domain.Interface.Service;
using DespesasCasa.Domain.Model.Dto;

namespace DespesasCasa.Application.Service;

public class UserService(IUnitOfWork _unitOfWork, IMapper _mapper) : IUserService
{
    public async Task<UserDto> GetUserAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(false, x => x.Id == userId);

        if (user == null)
        {
            throw new AppDomainException("User not found", ErrorCodeEnum.ObjectNotFound);
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        return _mapper.Map<List<UserDto>>(await _unitOfWork.UserRepository.GetAllAsync());
    }

    public async Task<Guid> CreateAsync(UserDto user)
    {
        var existing = await _unitOfWork.UserRepository.FindAsync(x => x.Email == user.Email);

        if (existing)
        {
            throw new AppDomainException("User already exists", ErrorCodeEnum.ObjectAlreadyExists);
        }

        var newUser = _mapper.Map<User>(user);
        newUser.Password = EncryptUtils.EncryptPassword(newUser.Password!);

        await _unitOfWork.UserRepository.AddAsync(newUser);
        await _unitOfWork.CommitAsync();

        return newUser.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(false, x => x.Id == id);

        if (user is null)
        {
            throw new AppDomainException("User not found", ErrorCodeEnum.ObjectNotFound);
        }

        _unitOfWork.UserRepository.Remove(user);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
