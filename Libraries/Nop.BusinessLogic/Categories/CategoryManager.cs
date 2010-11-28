using System;
using System.Collections.Generic;
using System.Linq;
using NopSolutions.NopCommerce.BusinessLogic.Caching;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Data;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.BusinessLogic.Categories
{
    /// <summary>
    /// Category manager
    /// </summary>
    public class CategoryManager
    {
        #region Constants

        private const string CATEGORIES_BY_ID_KEY = "Nop.category.id-{0}";
        private const string PRODUCTCATEGORIES_ALLBYCATEGORYID_KEY = "Nop.productcategory.allbycategoryid-{0}-{1}";
        private const string PRODUCTCATEGORIES_ALLBYPRODUCTID_KEY = "Nop.productcategory.allbyproductid-{0}-{1}";
        private const string PRODUCTCATEGORIES_BY_ID_KEY = "Nop.productcategory.id-{0}";
        private const string CATEGORIES_PATTERN_KEY = "Nop.category.";
        private const string PRODUCTCATEGORIES_PATTERN_KEY = "Nop.productcategory.";

        #endregion

        #region Methods

        /// <summary>
        /// Marks category as deleted
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        public static void MarkCategoryAsDeleted(int categoryId)
        {
            Category category = GetCategoryById(categoryId);
            if (category != null)
            {
                category = UpdateCategory(category.CategoryId, category.Name,
                                          category.Description, category.TemplateId, category.MetaKeywords,
                                          category.MetaDescription, category.MetaTitle, category.SEName,
                                          category.ParentCategoryId,
                                          category.PictureId, category.PageSize, category.PriceRanges,
                                          category.ShowOnHomePage,
                                          category.Published, true, category.DisplayOrder,
                                          category.CreatedOn, category.UpdatedOn);
            }
        }

        /// <summary>
        /// Removes category picture
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        public static void RemoveCategoryPicture(int categoryId)
        {
            Category category = GetCategoryById(categoryId);
            if (category != null)
            {
                UpdateCategory(category.CategoryId, category.Name, category.Description,
                               category.TemplateId, category.MetaKeywords,
                               category.MetaDescription, category.MetaTitle, category.SEName,
                               category.ParentCategoryId, 0, category.PageSize, category.PriceRanges,
                               category.ShowOnHomePage, category.Published, category.Deleted, category.DisplayOrder,
                               category.CreatedOn, category.UpdatedOn);
            }
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>Categories</returns>
        public static List<Category> GetAllCategories()
        {
            bool showHidden = NopContext.Current.IsAdmin;
            return GetAllCategories(showHidden);
        }


        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public static List<Category> GetAllCategories(bool showHidden)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<Category> query = from c in context.Categories
                                         orderby c.ParentCategoryId , c.DisplayOrder
                                         where (showHidden || c.Published) &&
                                               !c.Deleted
                                         select c;
            List<Category> unsortedCategories = query.ToList();

            //sort categories
            //TODO sort categories on database layer
            List<Category> sortedCategories = unsortedCategories.SortCategoriesForTree(0);

            return sortedCategories;
        }

        /// <summary>
        /// Gets all categories by parent category identifier
        /// </summary>
        /// <param name="parentCategoryId">Parent category identifier</param>
        /// <returns>Category collection</returns>
        public static List<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId)
        {
            bool showHidden = NopContext.Current.IsAdmin;
            return GetAllCategoriesByParentCategoryId(parentCategoryId, showHidden);
        }

        /// <summary>
        /// Gets all categories filtered by parent category identifier
        /// </summary>
        /// <param name="parentCategoryId">Parent category identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category collection</returns>
        public static List<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
                                                                        bool showHidden)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<Category> query = from c in context.Categories
                                         orderby c.DisplayOrder
                                         where (showHidden || c.Published) &&
                                               !c.Deleted &&
                                               c.ParentCategoryId == parentCategoryId
                                         select c;
            List<Category> categories = query.ToList();
            return categories;
        }

        /// <summary>
        /// Gets all categories displayed on the home page
        /// </summary>
        /// <returns>Category collection</returns>
        public static List<Category> GetAllCategoriesDisplayedOnHomePage()
        {
            bool showHidden = NopContext.Current.IsAdmin;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<Category> query = from c in context.Categories
                                         orderby c.DisplayOrder
                                         where (showHidden || c.Published) && !c.Deleted && c.ShowOnHomePage
                                         select c;
            List<Category> categories = query.ToList();
            return categories;
        }

        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        public static Category GetCategoryById(int categoryId)
        {
            if (categoryId == 0)
                return null;

            string key = string.Format(CATEGORIES_BY_ID_KEY, categoryId);
            object obj2 = NopRequestCache.Get(key);
            if (CategoriesCacheEnabled && (obj2 != null))
            {
                return (Category) obj2;
            }

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<Category> query = from c in context.Categories
                                         where c.CategoryId == categoryId
                                         select c;
            Category category = query.SingleOrDefault();

            if (CategoriesCacheEnabled)
            {
                NopRequestCache.Add(key, category);
            }
            return category;
        }

        /// <summary>
        /// Gets a category breadcrumb
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        public static List<Category> GetBreadCrumb(int categoryId)
        {
            var breadCrumb = new List<Category>();
            Category category = GetCategoryById(categoryId);
            while (category != null && !category.Deleted && category.Published)
            {
                breadCrumb.Add(category);
                category = category.ParentCategory;
            }
            breadCrumb.Reverse();
            return breadCrumb;
        }

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="name">The category name</param>
        /// <param name="description">The description</param>
        /// <param name="templateId">The template identifier</param>
        /// <param name="metaKeywords">The meta keywords</param>
        /// <param name="metaDescription">The meta description</param>
        /// <param name="metaTitle">The meta title</param>
        /// <param name="seName">The search-engine name</param>
        /// <param name="parentCategoryId">The parent category identifier</param>
        /// <param name="pictureId">The picture identifier</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="priceRanges">The price ranges</param>
        /// <param name="showOnHomePage">A value indicating whether the category will be shown on home page</param>
        /// <param name="published">A value indicating whether the entity is published</param>
        /// <param name="deleted">A value indicating whether the entity has been deleted</param>
        /// <param name="displayOrder">The display order</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <param name="updatedOn">The date and time of instance update</param>
        /// <returns>Category</returns>
        public static Category InsertCategory(string name, string description,
                                              int templateId, string metaKeywords, string metaDescription,
                                              string metaTitle,
                                              string seName, int parentCategoryId, int pictureId,
                                              int pageSize, string priceRanges, bool showOnHomePage, bool published,
                                              bool deleted,
                                              int displayOrder, DateTime createdOn, DateTime updatedOn)
        {
            name = CommonHelper.EnsureMaximumLength(name, 400);
            metaKeywords = CommonHelper.EnsureMaximumLength(metaKeywords, 400);
            metaDescription = CommonHelper.EnsureMaximumLength(metaDescription, 4000);
            metaTitle = CommonHelper.EnsureMaximumLength(metaTitle, 400);
            seName = CommonHelper.EnsureMaximumLength(seName, 100);
            priceRanges = CommonHelper.EnsureMaximumLength(priceRanges, 400);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            Category category = context.Categories.CreateObject();
            category.Name = name;
            category.Description = description;
            category.TemplateId = templateId;
            category.MetaKeywords = metaKeywords;
            category.MetaDescription = metaDescription;
            category.MetaTitle = metaTitle;
            category.SEName = seName;
            category.ParentCategoryId = parentCategoryId;
            category.PictureId = pictureId;
            category.PageSize = pageSize;
            category.PriceRanges = priceRanges;
            category.ShowOnHomePage = showOnHomePage;
            category.Published = published;
            category.Deleted = deleted;
            category.DisplayOrder = displayOrder;
            category.CreatedOn = createdOn;
            category.UpdatedOn = updatedOn;

            context.Categories.AddObject(category);
            context.SaveChanges();

            if (CategoriesCacheEnabled || MappingsCacheEnabled)
            {
                NopRequestCache.RemoveByPattern(CATEGORIES_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);
            }

            return category;
        }

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="name">The category name</param>
        /// <param name="description">The description</param>
        /// <param name="templateId">The template identifier</param>
        /// <param name="metaKeywords">The meta keywords</param>
        /// <param name="metaDescription">The meta description</param>
        /// <param name="metaTitle">The meta title</param>
        /// <param name="seName">The search-engine name</param>
        /// <param name="parentCategoryId">The parent category identifier</param>
        /// <param name="pictureId">The picture identifier</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="priceRanges">The price ranges</param>
        /// <param name="published">A value indicating whether the entity is published</param>
        /// <param name="showOnHomePage">A value indicating whether the category will be shown on home page</param>
        /// <param name="deleted">A value indicating whether the entity has been deleted</param>
        /// <param name="displayOrder">The display order</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <param name="updatedOn">The date and time of instance update</param>
        /// <returns>Category</returns>
        public static Category UpdateCategory(int categoryId, string name, string description,
                                              int templateId, string metaKeywords, string metaDescription,
                                              string metaTitle,
                                              string seName, int parentCategoryId, int pictureId,
                                              int pageSize, string priceRanges, bool showOnHomePage, bool published,
                                              bool deleted,
                                              int displayOrder, DateTime createdOn, DateTime updatedOn)
        {
            name = CommonHelper.EnsureMaximumLength(name, 400);
            metaKeywords = CommonHelper.EnsureMaximumLength(metaKeywords, 400);
            metaDescription = CommonHelper.EnsureMaximumLength(metaDescription, 4000);
            metaTitle = CommonHelper.EnsureMaximumLength(metaTitle, 400);
            seName = CommonHelper.EnsureMaximumLength(seName, 100);
            priceRanges = CommonHelper.EnsureMaximumLength(priceRanges, 400);

            //validate category hierarchy
            Category parentCategory = GetCategoryById(parentCategoryId);
            while (parentCategory != null)
            {
                if (categoryId == parentCategory.CategoryId)
                {
                    parentCategoryId = 0;
                    break;
                }
                parentCategory = GetCategoryById(parentCategory.ParentCategoryId);
            }

            Category category = GetCategoryById(categoryId);
            if (category == null)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(category))
                context.Categories.Attach(category);

            category.Name = name;
            category.Description = description;
            category.TemplateId = templateId;
            category.MetaKeywords = metaKeywords;
            category.MetaDescription = metaDescription;
            category.MetaTitle = metaTitle;
            category.SEName = seName;
            category.ParentCategoryId = parentCategoryId;
            category.PictureId = pictureId;
            category.PageSize = pageSize;
            category.PriceRanges = priceRanges;
            category.ShowOnHomePage = showOnHomePage;
            category.Published = published;
            category.Deleted = deleted;
            category.DisplayOrder = displayOrder;
            category.CreatedOn = createdOn;
            category.UpdatedOn = updatedOn;
            context.SaveChanges();

            if (CategoriesCacheEnabled || MappingsCacheEnabled)
            {
                NopRequestCache.RemoveByPattern(CATEGORIES_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);
            }

            return category;
        }

        /// <summary>
        /// Gets localized category by id
        /// </summary>
        /// <param name="categoryLocalizedId">Localized category identifier</param>
        /// <returns>Category content</returns>
        public static CategoryLocalized GetCategoryLocalizedById(int categoryLocalizedId)
        {
            if (categoryLocalizedId == 0)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<CategoryLocalized> query = from cl in context.CategoryLocalized
                                                  where cl.CategoryLocalizedId == categoryLocalizedId
                                                  select cl;
            CategoryLocalized categoryLocalized = query.SingleOrDefault();
            return categoryLocalized;
        }

        /// <summary>
        /// Gets localized category by category id
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category content</returns>
        public static List<CategoryLocalized> GetCategoryLocalizedByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                return new List<CategoryLocalized>();

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<CategoryLocalized> query = from cl in context.CategoryLocalized
                                                  where cl.CategoryId == categoryId
                                                  select cl;
            List<CategoryLocalized> content = query.ToList();
            return content;
        }

        /// <summary>
        /// Gets localized category by category id and language id
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Category content</returns>
        public static CategoryLocalized GetCategoryLocalizedByCategoryIdAndLanguageId(int categoryId, int languageId)
        {
            if (categoryId == 0 || languageId == 0)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<CategoryLocalized> query = from cl in context.CategoryLocalized
                                                  orderby cl.CategoryLocalizedId
                                                  where cl.CategoryId == categoryId &&
                                                        cl.LanguageId == languageId
                                                  select cl;
            CategoryLocalized categoryLocalized = query.FirstOrDefault();
            return categoryLocalized;
        }

        /// <summary>
        /// Inserts a localized category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="name">Name text</param>
        /// <param name="description">Description text</param>
        /// <param name="metaKeywords">Meta keywords text</param>
        /// <param name="metaDescription">Meta descriptions text</param>
        /// <param name="metaTitle">Metat title text</param>
        /// <param name="seName">Se Name text</param>
        /// <returns>Category content</returns>
        public static CategoryLocalized InsertCategoryLocalized(int categoryId,
                                                                int languageId, string name, string description,
                                                                string metaKeywords, string metaDescription,
                                                                string metaTitle,
                                                                string seName)
        {
            name = CommonHelper.EnsureMaximumLength(name, 400);
            metaKeywords = CommonHelper.EnsureMaximumLength(metaKeywords, 400);
            metaDescription = CommonHelper.EnsureMaximumLength(metaDescription, 4000);
            metaTitle = CommonHelper.EnsureMaximumLength(metaTitle, 400);
            seName = CommonHelper.EnsureMaximumLength(seName, 100);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            CategoryLocalized categoryLocalized = context.CategoryLocalized.CreateObject();
            categoryLocalized.CategoryId = categoryId;
            categoryLocalized.LanguageId = languageId;
            categoryLocalized.Name = name;
            categoryLocalized.Description = description;
            categoryLocalized.MetaKeywords = metaKeywords;
            categoryLocalized.MetaDescription = metaDescription;
            categoryLocalized.MetaTitle = metaTitle;
            categoryLocalized.SEName = seName;

            context.CategoryLocalized.AddObject(categoryLocalized);
            context.SaveChanges();

            if (CategoriesCacheEnabled)
            {
                NopRequestCache.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            }

            return categoryLocalized;
        }

        /// <summary>
        /// Update a localized category
        /// </summary>
        /// <param name="categoryLocalizedId">Localized category identifier</param>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="name">Name text</param>
        /// <param name="description">Description text</param>
        /// <param name="metaKeywords">Meta keywords text</param>
        /// <param name="metaDescription">Meta descriptions text</param>
        /// <param name="metaTitle">Metat title text</param>
        /// <param name="seName">Se Name text</param>
        /// <returns>Category content</returns>
        public static CategoryLocalized UpdateCategoryLocalized(int categoryLocalizedId,
                                                                int categoryId, int languageId, string name,
                                                                string description,
                                                                string metaKeywords, string metaDescription,
                                                                string metaTitle,
                                                                string seName)
        {
            name = CommonHelper.EnsureMaximumLength(name, 400);
            metaKeywords = CommonHelper.EnsureMaximumLength(metaKeywords, 400);
            metaDescription = CommonHelper.EnsureMaximumLength(metaDescription, 4000);
            metaTitle = CommonHelper.EnsureMaximumLength(metaTitle, 400);
            seName = CommonHelper.EnsureMaximumLength(seName, 100);

            CategoryLocalized categoryLocalized = GetCategoryLocalizedById(categoryLocalizedId);
            if (categoryLocalized == null)
                return null;

            bool allFieldsAreEmpty = string.IsNullOrEmpty(name) &&
                                     string.IsNullOrEmpty(description) &&
                                     string.IsNullOrEmpty(metaKeywords) &&
                                     string.IsNullOrEmpty(metaDescription) &&
                                     string.IsNullOrEmpty(metaTitle) &&
                                     string.IsNullOrEmpty(seName);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(categoryLocalized))
                context.CategoryLocalized.Attach(categoryLocalized);

            if (allFieldsAreEmpty)
            {
                //delete if all fields are empty
                context.DeleteObject(categoryLocalized);
                context.SaveChanges();
            }
            else
            {
                categoryLocalized.CategoryId = categoryId;
                categoryLocalized.LanguageId = languageId;
                categoryLocalized.Name = name;
                categoryLocalized.Description = description;
                categoryLocalized.MetaKeywords = metaKeywords;
                categoryLocalized.MetaDescription = metaDescription;
                categoryLocalized.MetaTitle = metaTitle;
                categoryLocalized.SEName = seName;
                context.SaveChanges();
            }

            if (CategoriesCacheEnabled)
            {
                NopRequestCache.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            }

            return categoryLocalized;
        }

        /// <summary>
        /// Deletes a product category mapping
        /// </summary>
        /// <param name="productCategoryId">Product category identifier</param>
        public static void DeleteProductCategory(int productCategoryId)
        {
            if (productCategoryId == 0)
                return;

            ProductCategory productCategory = GetProductCategoryById(productCategoryId);
            if (productCategory == null)
                return;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(productCategory))
                context.ProductCategories.Attach(productCategory);
            context.DeleteObject(productCategory);
            context.SaveChanges();

            if (CategoriesCacheEnabled || MappingsCacheEnabled)
            {
                NopRequestCache.RemoveByPattern(CATEGORIES_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);
            }
        }

        /// <summary>
        /// Gets product category mapping collection
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Product a category mapping collection</returns>
        public static List<ProductCategory> GetProductCategoriesByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                return new List<ProductCategory>();

            bool showHidden = NopContext.Current.IsAdmin;
            string key = string.Format(PRODUCTCATEGORIES_ALLBYCATEGORYID_KEY, showHidden, categoryId);
            object obj2 = NopRequestCache.Get(key);
            if (MappingsCacheEnabled && (obj2 != null))
            {
                return (List<ProductCategory>) obj2;
            }

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<ProductCategory> query = from pc in context.ProductCategories
                                                join p in context.Products on pc.ProductId equals p.ProductId
                                                where pc.CategoryId == categoryId &&
                                                      !p.Deleted &&
                                                      (showHidden || p.Published)
                                                orderby pc.DisplayOrder
                                                select pc;
            List<ProductCategory> productCategories = query.ToList();

            if (MappingsCacheEnabled)
            {
                NopRequestCache.Add(key, productCategories);
            }
            return productCategories;
        }

        /// <summary>
        /// Gets a product category mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product category mapping collection</returns>
        public static List<ProductCategory> GetProductCategoriesByProductId(int productId)
        {
            if (productId == 0)
                return new List<ProductCategory>();

            bool showHidden = NopContext.Current.IsAdmin;
            string key = string.Format(PRODUCTCATEGORIES_ALLBYPRODUCTID_KEY, showHidden, productId);
            object obj2 = NopRequestCache.Get(key);
            if (MappingsCacheEnabled && (obj2 != null))
            {
                return (List<ProductCategory>) obj2;
            }

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<ProductCategory> query = from pc in context.ProductCategories
                                                join c in context.Categories on pc.CategoryId equals c.CategoryId
                                                where pc.ProductId == productId &&
                                                      !c.Deleted &&
                                                      (showHidden || c.Published)
                                                orderby pc.DisplayOrder
                                                select pc;
            List<ProductCategory> productCategories = query.ToList();

            if (MappingsCacheEnabled)
            {
                NopRequestCache.Add(key, productCategories);
            }
            return productCategories;
        }

        /// <summary>
        /// Gets a product category mapping 
        /// </summary>
        /// <param name="productCategoryId">Product category mapping identifier</param>
        /// <returns>Product category mapping</returns>
        public static ProductCategory GetProductCategoryById(int productCategoryId)
        {
            if (productCategoryId == 0)
                return null;

            string key = string.Format(PRODUCTCATEGORIES_BY_ID_KEY, productCategoryId);
            object obj2 = NopRequestCache.Get(key);
            if (MappingsCacheEnabled && (obj2 != null))
            {
                return (ProductCategory) obj2;
            }

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<ProductCategory> query = from pc in context.ProductCategories
                                                where pc.ProductCategoryId == productCategoryId
                                                select pc;
            ProductCategory productCategory = query.SingleOrDefault();

            if (MappingsCacheEnabled)
            {
                NopRequestCache.Add(key, productCategory);
            }
            return productCategory;
        }

        /// <summary>
        /// Inserts a product category mapping
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="isFeaturedProduct">A value indicating whether the product is featured</param>
        /// <param name="displayOrder">The display order</param>
        /// <returns>Product category mapping </returns>
        public static ProductCategory InsertProductCategory(int productId, int categoryId,
                                                            bool isFeaturedProduct, int displayOrder)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            ProductCategory productCategory = context.ProductCategories.CreateObject();
            productCategory.ProductId = productId;
            productCategory.CategoryId = categoryId;
            productCategory.IsFeaturedProduct = isFeaturedProduct;
            productCategory.DisplayOrder = displayOrder;

            context.ProductCategories.AddObject(productCategory);
            context.SaveChanges();

            if (CategoriesCacheEnabled || MappingsCacheEnabled)
            {
                NopRequestCache.RemoveByPattern(CATEGORIES_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);
            }
            return productCategory;
        }

        /// <summary>
        /// Updates the product category mapping 
        /// </summary>
        /// <param name="productCategoryId">Product category mapping  identifier</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="isFeaturedProduct">A value indicating whether the product is featured</param>
        /// <param name="displayOrder">The display order</param>
        /// <returns>Product category mapping </returns>
        public static ProductCategory UpdateProductCategory(int productCategoryId,
                                                            int productId, int categoryId, bool isFeaturedProduct,
                                                            int displayOrder)
        {
            ProductCategory productCategory = GetProductCategoryById(productCategoryId);
            if (productCategory == null)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(productCategory))
                context.ProductCategories.Attach(productCategory);

            productCategory.ProductId = productId;
            productCategory.CategoryId = categoryId;
            productCategory.IsFeaturedProduct = isFeaturedProduct;
            productCategory.DisplayOrder = displayOrder;
            context.SaveChanges();

            if (CategoriesCacheEnabled || MappingsCacheEnabled)
            {
                NopRequestCache.RemoveByPattern(CATEGORIES_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);
            }
            return productCategory;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether categories cache is enabled
        /// </summary>
        public static bool CategoriesCacheEnabled
        {
            get { return SettingManager.GetSettingValueBoolean("Cache.CategoryManager.CategoriesCacheEnabled"); }
        }

        /// <summary>
        /// Gets a value indicating whether mappings cache is enabled
        /// </summary>
        public static bool MappingsCacheEnabled
        {
            get { return SettingManager.GetSettingValueBoolean("Cache.CategoryManager.MappingsCacheEnabled"); }
        }

        #endregion
    }
}