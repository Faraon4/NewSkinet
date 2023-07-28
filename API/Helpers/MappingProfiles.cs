using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        // Installed  AutoMapper.Dependency Injection -> and all mapping are done automatically
        // this is a new packet , it is not as before


        // Mapping is need to be done for brand and type , because in the Product class -> they are a type of a class
        // With this , we are showing that we want to take only the the name of the type and brand

        // d -> destination
        // o -> options
        // s -> source
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d =>d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d =>d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());


            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>(); // This are used for validation of the basket
            CreateMap<BasketItemDto,BasketItem>(); // This are used for validation of the basket
        }
    }
}