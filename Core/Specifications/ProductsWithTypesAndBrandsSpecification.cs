using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
       // We need to create an empty contructor in the BaseSpecification class , to remove the error that appear when we create this class



       public ProductsWithTypesAndBrandsSpecification()
       {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
       }
    }
}