using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    // Product -> what do we want to map
    // PrDtop -> to what we want to map
    // string -> return type
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        
        
        //IConfiguration -> should come from Microsoft.Extensions.Configuration
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
            
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            
            return null;
        }
    }
}