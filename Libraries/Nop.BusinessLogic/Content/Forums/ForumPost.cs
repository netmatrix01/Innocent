using System;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Forums
{
    /// <summary>
    /// Represents a forum post
    /// </summary>
    public class ForumPost : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the forum post identifier
        /// </summary>
        public int ForumPostId { get; set; }

        /// <summary>
        /// Gets or sets the forum topic identifier
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the topic
        /// </summary>
        public ForumTopic Topic
        {
            get { return ForumManager.GetTopicById(TopicId); }
        }

        /// <summary>
        /// Gets the user
        /// </summary>
        public Customer User
        {
            get { return CustomerManager.GetCustomerById(UserId); }
        }

        #endregion
    }
}