using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    // Class where we will put parameters that will be used in the controller for get products
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex {get;set;} = 1; // by default we want to return first page
        private int _pageSize = 6;
        public int PageSize 
        { 
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? BrandId { get; set; }
         public int? TypeId { get; set; }
         public string Sort { get; set; }

         //Seach functionality
         // We are doing such way because , even if in the search bar there will be uppercase leeter , they will be converted to the lower case
        private string _search;
        public string Search 
        { get => _search; 
          set => _search = value.ToLower();
        }

    }
}