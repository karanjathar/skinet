using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithFiltersCountWithSpecification:BaseSpecification<Product>
    {
        public ProductWithFiltersCountWithSpecification(ProductSpecParams productParams) : base(x =>
        (string.IsNullOrEmpty(productParams._search) || x.Name.ToLower().Contains(productParams._search))
        &&
        (!productParams.brandId.HasValue || x.ProductBrandId == productParams.brandId) && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {
        
        }
    }
}
