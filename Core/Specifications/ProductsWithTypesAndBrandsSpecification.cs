using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
       // We need to create an empty contructor in the BaseSpecification class , to remove the error that appear when we create this class


        // We use this ctor to get the list of products with types and brands
       public ProductsWithTypesAndBrandsSpecification()
       {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
       }

        // Contructor to get ONE PRODUCT with type and brand
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}