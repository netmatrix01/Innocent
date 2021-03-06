using System;
using System.Collections.Generic;
using System.Linq;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Discounts;
using NopSolutions.NopCommerce.BusinessLogic.Templates;

namespace NopSolutions.NopCommerce.BusinessLogic.Categories
{
    /// <summary>
    /// Represents a category
    /// </summary>
    public class Category : BaseEntity
    {
        #region Fields

        private List<CategoryLocalized> _categoryLocalized;

        #endregion

        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the template identifier
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the search-engine name
        /// </summary>
        public string SEName { get; set; }

        /// <summary>
        /// Gets or sets the parent category identifier
        /// </summary>
        public int ParentCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the available price ranges
        /// </summary>
        public string PriceRanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the category on home page
        /// </summary>
        public bool ShowOnHomePage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        #endregion

        #region Localizable methods/properties

        /// <summary>
        /// Gets the localized name 
        /// </summary>
        public string LocalizedName
        {
            get { return GetLocalizedName(NopContext.Current.WorkingLanguage.LanguageId); }
        }

        /// <summary>
        /// Gets the localized description 
        /// </summary>
        public string LocalizedDescription
        {
            get { return GetLocalizedDescription(NopContext.Current.WorkingLanguage.LanguageId); }
        }

        /// <summary>
        /// Gets the localized meta keywords 
        /// </summary>
        public string LocalizedMetaKeywords
        {
            get { return GetLocalizedMetaKeywords(NopContext.Current.WorkingLanguage.LanguageId); }
        }

        /// <summary>
        /// Gets the localized meta description
        /// </summary>
        public string LocalizedMetaDescription
        {
            get { return GetLocalizedMetaDescription(NopContext.Current.WorkingLanguage.LanguageId); }
        }

        /// <summary>
        /// Gets the localized meta title 
        /// </summary>
        public string LocalizedMetaTitle
        {
            get { return GetLocalizedMetaTitle(NopContext.Current.WorkingLanguage.LanguageId); }
        }

        /// <summary>
        /// Gets the localized search-engine name 
        /// </summary>
        public string LocalizedSEName
        {
            get { return GetLocalizedSEName(NopContext.Current.WorkingLanguage.LanguageId); }
        }

        /// <summary>
        /// Gets the localized name
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized name</returns>
        public string GetLocalizedName(int languageId)
        {
            if (NopContext.Current.LocalizedEntityPropertiesEnabled)
            {
                if (languageId > 0)
                {
                    if (_categoryLocalized == null)
                        _categoryLocalized = CategoryManager.GetCategoryLocalizedByCategoryId(CategoryId);

                    CategoryLocalized temp1 = _categoryLocalized.FirstOrDefault(cl => cl.LanguageId == languageId);
                    if (temp1 != null && !String.IsNullOrWhiteSpace(temp1.Name))
                        return temp1.Name;
                }
            }

            return Name;
        }

        /// <summary>
        /// Gets the localized description
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized description</returns>
        public string GetLocalizedDescription(int languageId)
        {
            if (NopContext.Current.LocalizedEntityPropertiesEnabled)
            {
                if (languageId > 0)
                {
                    if (_categoryLocalized == null)
                        _categoryLocalized = CategoryManager.GetCategoryLocalizedByCategoryId(CategoryId);

                    CategoryLocalized temp1 = _categoryLocalized.FirstOrDefault(cl => cl.LanguageId == languageId);
                    if (temp1 != null && !String.IsNullOrWhiteSpace(temp1.Description))
                        return temp1.Description;
                }
            }

            return Description;
        }

        /// <summary>
        /// Gets the localized meta keywords
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized meta keywords</returns>
        public string GetLocalizedMetaKeywords(int languageId)
        {
            if (NopContext.Current.LocalizedEntityPropertiesEnabled)
            {
                if (languageId > 0)
                {
                    if (_categoryLocalized == null)
                        _categoryLocalized = CategoryManager.GetCategoryLocalizedByCategoryId(CategoryId);

                    CategoryLocalized temp1 = _categoryLocalized.FirstOrDefault(cl => cl.LanguageId == languageId);
                    if (temp1 != null && !String.IsNullOrWhiteSpace(temp1.MetaKeywords))
                        return temp1.MetaKeywords;
                }
            }

            return MetaKeywords;
        }

        /// <summary>
        /// Gets the localized meta description
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized meta description</returns>
        public string GetLocalizedMetaDescription(int languageId)
        {
            if (NopContext.Current.LocalizedEntityPropertiesEnabled)
            {
                if (languageId > 0)
                {
                    if (_categoryLocalized == null)
                        _categoryLocalized = CategoryManager.GetCategoryLocalizedByCategoryId(CategoryId);

                    CategoryLocalized temp1 = _categoryLocalized.FirstOrDefault(cl => cl.LanguageId == languageId);
                    if (temp1 != null && !String.IsNullOrWhiteSpace(temp1.MetaDescription))
                        return temp1.MetaDescription;
                }
            }

            return MetaDescription;
        }

        /// <summary>
        /// Gets the localized meta title 
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized meta title </returns>
        public string GetLocalizedMetaTitle(int languageId)
        {
            if (NopContext.Current.LocalizedEntityPropertiesEnabled)
            {
                if (languageId > 0)
                {
                    if (_categoryLocalized == null)
                        _categoryLocalized = CategoryManager.GetCategoryLocalizedByCategoryId(CategoryId);

                    CategoryLocalized temp1 = _categoryLocalized.FirstOrDefault(cl => cl.LanguageId == languageId);
                    if (temp1 != null && !String.IsNullOrWhiteSpace(temp1.MetaTitle))
                        return temp1.MetaTitle;
                }
            }

            return MetaTitle;
        }

        /// <summary>
        /// Gets the localized search-engine name
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized search-engine name</returns>
        public string GetLocalizedSEName(int languageId)
        {
            if (NopContext.Current.LocalizedEntityPropertiesEnabled)
            {
                if (languageId > 0)
                {
                    if (_categoryLocalized == null)
                        _categoryLocalized = CategoryManager.GetCategoryLocalizedByCategoryId(CategoryId);

                    CategoryLocalized temp1 = _categoryLocalized.FirstOrDefault(cl => cl.LanguageId == languageId);
                    if (temp1 != null && !String.IsNullOrWhiteSpace(temp1.SEName))
                        return temp1.SEName;
                }
            }

            return SEName;
        }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the parent category
        /// </summary>
        public Category ParentCategory
        {
            get { return CategoryManager.GetCategoryById(ParentCategoryId); }
        }

        /// <summary>
        /// Gets the category template
        /// </summary>
        public CategoryTemplate CategoryTemplate
        {
            get { return TemplateManager.GetCategoryTemplateById(TemplateId); }
        }

        /// <summary>
        /// Gets the products
        /// </summary>
        public List<ProductCategory> ProductCategories
        {
            get { return CategoryManager.GetProductCategoriesByCategoryId(CategoryId); }
        }

        /// <summary>
        /// Gets the picture
        /// </summary>
        public Picture Picture
        {
            get { return PictureManager.GetPictureById(PictureId); }
        }

        /// <summary>
        /// Gets the discounts of the category
        /// </summary>
        public List<Discount> Discounts
        {
            get { return DiscountManager.GetDiscountsByCategoryId(CategoryId); }
        }

        /// <summary>
        /// Gets the featured products of the category
        /// </summary>
        public List<Product> FeaturedProducts
        {
            get
            {
                int totalFeaturedRecords = 0;
                List<Product> featuredProducts = ProductManager.GetAllProducts(CategoryId,
                                                                               0, 0, true, int.MaxValue - 1, 0,
                                                                               out totalFeaturedRecords);
                return featuredProducts;
            }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the localized category
        /// </summary>
        public virtual ICollection<CategoryLocalized> NpCategoryLocalized { get; set; }

        /// <summary>
        /// Gets the discount
        /// </summary>
        public virtual ICollection<Discount> NpDiscounts { get; set; }

        /// <summary>
        /// Gets the product categories
        /// </summary>
        public virtual ICollection<ProductCategory> NpProductCategories { get; set; }

        #endregion
    }
}