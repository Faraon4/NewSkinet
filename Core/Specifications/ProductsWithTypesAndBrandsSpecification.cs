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
       public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) 

       // Because we already have a where operation for our Criteria in the Evaluator , we need to do like this in the base ctor
        : base(x => 
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
       {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        //Default order if we do not have any sort
        AddOrderBy(x => x.Name);

        //Adding the pagination
        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize); // This is the formula that we need to use


      // sort can be priceAsc or priceDesc----> and base on this we write what to do based on the Evaluator

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;


                default :
                    AddOrderBy(n => n.Name);
                    break;
            }
        }
       }

        // Contructor to get ONE PRODUCT with type and brand
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}