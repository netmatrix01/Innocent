using System;
using System.Collections.Generic;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Forums
{
    /// <summary>
    /// Represents a forum topic
    /// </summary>
    public class ForumTopic : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the forum topic identifier
        /// </summary>
        public int ForumTopicId { get; set; }

        /// <summary>
        /// Gets or sets the forum identifier
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the topic type identifier
        /// </summary>
        public int TopicTypeId { get; set; }

        /// <summary>
        /// Gets or sets the subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the number of posts
        /// </summary>
        public int NumPosts { get; set; }

        /// <summary>
        /// Gets or sets the number of views
        /// </summary>
        public int Views { get; set; }

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
        /// Gets the number of replies
        /// </summary>
        public int NumReplies
        {
            get
            {
                int result = 0;
                if (NumPosts > 0)
                    result = NumPosts - 1;
                return result;
            }
        }

        /// <summary>
        /// Gets the forum
        /// </summary>
        public Forum Forum
        {
            get { return ForumManager.GetForumById(ForumId); }
        }

        /// <summary>
        /// Gets the user
        /// </summary>
        public Customer User
        {
            get { return CustomerManager.GetCustomerById(UserId); }
        }

        /// <summary>
        /// Gets the log type
        /// </summary>
        public ForumTopicTypeEnum TopicType
        {
            get { return (ForumTopicTypeEnum) TopicTypeId; }
        }

        /// <summary>
        /// Gets the first post
        /// </summary>
        public ForumPost FirstPost
        {
            get
            {
                int totalPostRecords = 0;
                List<ForumPost> forumPosts = ForumManager.GetAllPosts(ForumTopicId, 0, string.Empty, 1, 0,
                                                                      out totalPostRecords);
                if (forumPosts.Count > 0)
                {
                    return forumPosts[0];
                }

                return null;
            }
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