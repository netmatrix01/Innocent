using NopSolutions.NopCommerce.BusinessLogic.Products;

namespace NopSolutions.NopCommerce.BusinessLogic.Categories
{
    /// <summary>
    /// Represents a product category mapping
    /// </summary>
    public class ProductCategory : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ProductCategory identifier
        /// </summary>
        public int ProductCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is featured
        /// </summary>
        public bool IsFeaturedProduct { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the category
        /// </summary>
        public Category Category
        {
            get { return CategoryManager.GetCategoryById(CategoryId); }
        }

        /// <summary>
        /// Gets the product
        /// </summary>
        public Product Product
        {
            get { return ProductManager.GetProductById(ProductId); }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual Category NpCategory { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Product NpProduct { get; set; }

        #endregion
    }
}