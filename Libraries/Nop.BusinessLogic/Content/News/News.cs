using System;
using System.Collections.Generic;
using NopSolutions.NopCommerce.BusinessLogic.Directory;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.NewsManagement
{
    /// <summary>
    /// Represents a news
    /// </summary>
    public class News : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the news identifier
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the news title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the short text
        /// </summary>
        public string Short { get; set; }

        /// <summary>
        /// Gets or sets the full text
        /// </summary>
        public string Full { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity allows comments
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the language
        /// </summary>
        public Language Language
        {
            get { return LanguageManager.GetLanguageById(LanguageId); }
        }

        /// <summary>
        /// Gets the news comments
        /// </summary>
        public List<NewsComment> NewsComments
        {
            get { return NewsManager.GetNewsCommentsByNewsId(NewsId); }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the news comments
        /// </summary>
        public virtual ICollection<NewsComment> NpNewsComments { get; set; }

        /// <summary>
        /// Gets the language
        /// </summary>
        public virtual Language NpLanguage { get; set; }

        #endregion
    }
}