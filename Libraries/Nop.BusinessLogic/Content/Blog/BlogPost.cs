using System;
using System.Collections.Generic;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Blog
{
    /// <summary>
    /// Represents a blog post
    /// </summary>
    public class BlogPost : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the blog post identifier
        /// </summary>
        public int BlogPostId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the blog post title
        /// </summary>
        public string BlogPostTitle { get; set; }

        /// <summary>
        /// Gets or sets the blog post title
        /// </summary>
        public string BlogPostBody { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the blog post comments are allowed 
        /// </summary>
        public bool BlogPostAllowComments { get; set; }

        /// <summary>
        /// Gets or sets the blog tags
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the user identifier who created the blog post
        /// </summary>
        public int CreatedById { get; set; }

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
        /// Gets the user who created the blog post
        /// </summary>
        public Customer CreatedBy
        {
            get { return CustomerManager.GetCustomerById(CreatedById); }
        }

        /// <summary>
        /// Gets the blog comment collection
        /// </summary>
        public List<BlogComment> BlogComments
        {
            get { return BlogManager.GetBlogCommentsByBlogPostId(BlogPostId); }
        }

        /// <summary>
        /// Gets the parsed blog post tags
        /// </summary>
        public string[] ParsedTags
        {
            get
            {
                var parsedTags = new List<string>();
                if (!String.IsNullOrEmpty(Tags))
                {
                    string[] tags2 = Tags.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string tag2 in tags2)
                    {
                        parsedTags.Add(tag2.Trim());
                    }
                }
                return parsedTags.ToArray();
            }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the blog comments
        /// </summary>
        public virtual ICollection<BlogComment> NpBlogComments { get; set; }

        /// <summary>
        /// Gets the user who created the blog post
        /// </summary>
        public virtual Customer NpCreatedBy { get; set; }

        /// <summary>
        /// Gets the language
        /// </summary>
        public virtual Language NpLanguage { get; set; }

        #endregion
    }
}