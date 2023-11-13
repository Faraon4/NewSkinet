using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

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


            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>(); // This are used for validation of the basket
            CreateMap<BasketItemDto,BasketItem>(); // This are used for validation of the basket
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>(); // We need this address for orders, it is complitly different with the address upper , line 32
            
            // Order need to be imported from the ..Entities.OrderAggregate
            CreateMap<Order, OrderToReturnDto>()
                     .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                     .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                     .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ItemOrdered.ProductItemId))
                     .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ItemOrdered.ProductName))
                     .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.ItemOrdered.PictureUrl));
        }
    }
}