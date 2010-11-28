using System.Collections.Generic;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Topics
{
    /// <summary>
    /// Represents a topic
    /// </summary>
    public class Topic : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the topic identifier
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether this topic is password proceted
        /// </summary>
        public bool IsPasswordProtected { get; set; }

        /// <summary>
        /// Gets or sets the password to access the content of this topic
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether this topic should be included in sitemap
        /// </summary>
        public bool IncludeInSitemap { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the localized topic
        /// </summary>
        public virtual ICollection<LocalizedTopic> NpLocalizedTopics { get; set; }

        #endregion
    }
}