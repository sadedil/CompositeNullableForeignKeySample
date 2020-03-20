using System;
using System.Collections.Generic;

namespace CompositeNullableForeignKeySample.Dtos
{
    public class ProductCategoryDto : HasTenantDto
    {
        public Guid ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; }

        public virtual ICollection<ProductDto> Products { get; set; }

        public override string ToString()
        {
            return $"Id={ProductCategoryId}, Name={ProductCategoryName}, ProductCount={Products?.Count}";
        }
    }
}