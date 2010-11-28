using System;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Forums
{
    /// <summary>
    /// Represents a forum subscription item
    /// </summary>
    public class ForumSubscription : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the forum subscription identifier
        /// </summary>
        public int ForumSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the forum subscription identifier
        /// </summary>
        public Guid SubscriptionGuid { get; set; }

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the forum identifier
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// Gets or sets the topic identifier
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the user
        /// </summary>
        public Customer User
        {
            get { return CustomerManager.GetCustomerById(UserId); }
        }

        /// <summary>
        /// Gets the forum
        /// </summary>
        public Forum Forum
        {
            get { return ForumManager.GetForumById(ForumId); }
        }

        /// <summary>
        /// Gets the topic
        /// </summary>
        public ForumTopic Topic
        {
            get { return ForumManager.GetTopicById(TopicId); }
        }

        #endregion
    }
}