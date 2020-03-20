using System;

namespace CompositeNullableForeignKeySample.Models
{
    public class Product : HasTenantModel
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public Guid? ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public override string ToString()
        {
            return $"Id={ProductId}, Name={ProductName}, CategoryName={ProductCategory?.ProductCategoryName}";
        }
    }
}