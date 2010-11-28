namespace NopSolutions.NopCommerce.BusinessLogic.Categories
{
    /// <summary>
    /// Represents a localized category
    /// </summary>
    public class CategoryLocalized : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the localized category identifier
        /// </summary>
        public int CategoryLocalizedId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

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

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual Category NpCategory { get; set; }

        #endregion
    }
}