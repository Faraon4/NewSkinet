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
       public ProductsWithTypesAndBrandsSpecification(string sort, int? brandId, int?typeId) 

       // Because we already have a where operation for our Criteria in the Evaluator , we need to do like this in the base ctor
        : base(x => 
            (!brandId.HasValue || x.ProductBrandId == brandId) &&
            (!typeId.HasValue || x.ProductTypeId == typeId)
        )
       {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        //Default order if we do not have any sort
        AddOrderBy(x => x.Name);


      // sort can be priceAsc or priceDesc----> and base on this we write what to do based on the Evaluator

        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
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