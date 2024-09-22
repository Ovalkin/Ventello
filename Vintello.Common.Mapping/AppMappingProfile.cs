using AutoMapper;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Category, RetrivedCategoriesDto>().ReverseMap();
        CreateMap<Category, RetrivedCategoryDto>()
            .ForMember(c => c.Items, expression => expression.MapFrom(src => src.Items))
            .ReverseMap();
        CreateMap<Category, CreatedCategoryDto>().ReverseMap();
        CreateMap<Category, UpdatedCategoryDto>().ReverseMap();
        

        CreateMap<CreateItemDto, Item>().ReverseMap();
        CreateMap<RetriveAllItemDto, Item>().ReverseMap();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<CreateUserDto, User>();
    }
}