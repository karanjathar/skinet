using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithTypesBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesBrandsSpecification(ProductSpecParams productParams) : base(
            x => (string.IsNullOrEmpty(productParams._search) ||x.Name.ToLower().Contains(productParams._search)) && 
            (!productParams.brandId.HasValue || x.ProductBrandId== productParams.brandId) 
            && (!productParams.TypeId.HasValue || x.ProductTypeId== productParams.TypeId)
            
            
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            AddOrderByDescending(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!String.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "priceAsc":AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc": AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductWithTypesBrandsSpecification(int id) : base(x=>x.Id==id)
        {

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
