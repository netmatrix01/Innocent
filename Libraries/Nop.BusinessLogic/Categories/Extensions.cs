using System.Collections.Generic;

namespace NopSolutions.NopCommerce.BusinessLogic.Categories
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns a ProductCategory that has the specified values
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>A ProductCategory that has the specified values; otherwise null</returns>
        public static ProductCategory FindProductCategory(this List<ProductCategory> source,
                                                          int productId, int categoryId)
        {
            foreach (ProductCategory productCategory in source)
                if (productCategory.ProductId == productId && productCategory.CategoryId == categoryId)
                    return productCategory;
            return null;
        }

        /// <summary>
        /// Sort categories for tree representation
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>Sorted categories</returns>
        public static List<Category> SortCategoriesForTree(this List<Category> source, int parentId)
        {
            var result = new List<Category>();

            List<Category> temp = source.FindAll(c => c.ParentCategoryId == parentId);
            foreach (Category cat in temp)
            {
                result.Add(cat);
                result.AddRange(SortCategoriesForTree(source, cat.CategoryId));
            }
            return result;
        }
    }
}