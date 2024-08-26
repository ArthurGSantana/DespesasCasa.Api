using AutoMapper;
using DespesasCasa.Domain.Entity;
using DespesasCasa.Domain.Model.Dto;

namespace DespesasCasa.Domain.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
