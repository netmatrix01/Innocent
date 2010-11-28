using System;
using System.Collections.Generic;
using System.Text;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;


namespace NopSolutions.NopCommerce.BusinessLogic.Content.Blog
{
    /// <summary>
    /// Represents a blog comment
    /// </summary>
    public partial class BlogComment : BaseEntity
    {
        #region Ctor
        /// <summary>
        /// Creates a new instance of the BlogComment class
        /// </summary>
        public BlogComment()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the blog comment identifier
        /// </summary>
        public int BlogCommentId { get; set; }

        /// <summary>
        /// Gets or sets the blog post identifier
        /// </summary>
        public int BlogPostId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier who commented the blog post
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the comment text
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the customer who commented the blog post
        /// </summary>
        public Customer Customer
        {
            get
            {
                return CustomerManager.GetCustomerById(this.CustomerId);
            }
        }

        /// <summary>
        /// Gets the blog comment collection
        /// </summary>
        public BlogPost BlogPost
        {
            get
            {
                return BlogManager.GetBlogPostById(this.BlogPostId);
            }
        }
        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the blog post
        /// </summary>
        public virtual BlogPost NpBlogPost { get; set; }

        #endregion
    }

}
