using System;
using System.Collections.Generic;

namespace CompositeNullableForeignKeySample.Models
{
    public class ProductCategory : HasTenantModel
    {
        public Guid ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override string ToString()
        {
            return $"Id={ProductCategoryId}, Name={ProductCategoryName}, ProductCount={Products?.Count}";
        }
    }
}