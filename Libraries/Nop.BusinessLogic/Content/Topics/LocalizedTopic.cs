using System;
using NopSolutions.NopCommerce.BusinessLogic.Directory;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Topics
{
    /// <summary>
    /// Represents a localized topic
    /// </summary>
    public class LocalizedTopic : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the localized topic identifier
        /// </summary>
        public int TopicLocalizedId { get; set; }

        /// <summary>
        /// Gets or sets the topic identifier
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOn { get; set; }

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
        /// Gets the topic
        /// </summary>
        public Topic Topic
        {
            get { return TopicManager.GetTopicById(TopicId); }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the topic
        /// </summary>
        public virtual Topic NpTopic { get; set; }

        #endregion
    }
}