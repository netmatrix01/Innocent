using System;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Forums
{
    /// <summary>
    /// Represents a forum
    /// </summary>
    public class Forum : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the forum identifier
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// Gets or sets the forum group identifier
        /// </summary>
        public int ForumGroupId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the number of topics
        /// </summary>
        public int NumTopics { get; set; }

        /// <summary>
        /// Gets or sets the number of posts
        /// </summary>
        public int NumPosts { get; set; }

        /// <summary>
        /// Gets or sets the last topic identifier
        /// </summary>
        public int LastTopicId { get; set; }

        /// <summary>
        /// Gets or sets the last post identifier
        /// </summary>
        public int LastPostId { get; set; }

        /// <summary>
        /// Gets or sets the last post user identifier
        /// </summary>
        public int LastPostUserId { get; set; }

        /// <summary>
        /// Gets or sets the last post date and time
        /// </summary>
        public DateTime? LastPostTime { get; set; }

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

        #region Custom Properties

        /// <summary>
        /// Gets the forum group
        /// </summary>
        public ForumGroup ForumGroup
        {
            get { return ForumManager.GetForumGroupById(ForumGroupId); }
        }

        /// <summary>
        /// Gets the last topic
        /// </summary>
        public ForumTopic LastTopic
        {
            get { return ForumManager.GetTopicById(LastTopicId); }
        }

        /// <summary>
        /// Gets the last post
        /// </summary>
        public ForumPost LastPost
        {
            get { return ForumManager.GetPostById(LastPostId); }
        }

        /// <summary>
        /// Gets the last post user
        /// </summary>
        public Customer LastPostUser
        {
            get { return CustomerManager.GetCustomerById(LastPostUserId); }
        }

        #endregion
    }
}