using System;

namespace CompositeNullableForeignKeySample.Dtos
{
    public class ProductDto : HasTenantDto
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public Guid? ProductCategoryId { get; set; }

        public virtual ProductCategoryDto ProductCategory { get; set; }

        public override string ToString()
        {
            return $"Id={ProductId}, Name={ProductName}, CategoryName={ProductCategory?.ProductCategoryName}";
        }
    }
}