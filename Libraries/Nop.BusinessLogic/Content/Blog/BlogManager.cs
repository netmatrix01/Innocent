using System;
using System.Collections.Generic;
using System.Linq;
using NopSolutions.NopCommerce.BusinessLogic.Caching;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Data;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Common.Utils.Html;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Blog
{
    /// <summary>
    /// Blog post manager
    /// </summary>
    public class BlogManager
    {
        #region Constants

        private const string BLOGPOST_BY_ID_KEY = "Nop.blogpost.id-{0}";
        private const string BLOGPOST_PATTERN_KEY = "Nop.blogpost.";

        #endregion

        #region Methods

        /// <summary>
        /// Deletes an blog post
        /// </summary>
        /// <param name="blogPostId">Blog post identifier</param>
        public static void DeleteBlogPost(int blogPostId)
        {
            BlogPost blogPost = GetBlogPostById(blogPostId);
            if (blogPost == null)
                return;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(blogPost))
                context.BlogPosts.Attach(blogPost);
            context.DeleteObject(blogPost);
            context.SaveChanges();

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(BLOGPOST_PATTERN_KEY);
            }
        }

        /// <summary>
        /// Gets an blog post
        /// </summary>
        /// <param name="blogPostId">Blog post identifier</param>
        /// <returns>Blog post</returns>
        public static BlogPost GetBlogPostById(int blogPostId)
        {
            if (blogPostId == 0)
                return null;

            string key = string.Format(BLOGPOST_BY_ID_KEY, blogPostId);
            object obj2 = NopRequestCache.Get(key);
            if (CacheEnabled && (obj2 != null))
            {
                return (BlogPost) obj2;
            }

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<BlogPost> query = from bp in context.BlogPosts
                                         where bp.BlogPostId == blogPostId
                                         select bp;
            BlogPost blogPost = query.SingleOrDefault();

            if (CacheEnabled)
            {
                NopRequestCache.Add(key, blogPost);
            }
            return blogPost;
        }

        /// <summary>
        /// Gets all blog posts
        /// </summary>
        /// <param name="languageId">Language identifier. 0 if you want to get all records</param>
        /// <returns>Blog posts</returns>
        public static List<BlogPost> GetAllBlogPosts(int languageId)
        {
            int totalRecords;
            return GetAllBlogPosts(languageId, Int32.MaxValue, 0, out totalRecords);
        }

        /// <summary>
        /// Gets all blog posts
        /// </summary>
        /// <param name="languageId">Language identifier. 0 if you want to get all records</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="totalRecords">Total records</param>
        /// <returns>Blog posts</returns>
        public static List<BlogPost> GetAllBlogPosts(int languageId, int pageSize,
                                                     int pageIndex, out int totalRecords)
        {
            return GetAllBlogPosts(languageId,
                                   null, null, pageSize, pageIndex, out totalRecords);
        }

        /// <summary>
        /// Gets all blog posts
        /// </summary>
        /// <param name="languageId">Language identifier; 0 if you want to get all records</param>
        /// <param name="dateFrom">Filter by created date; null if you want to get all records</param>
        /// <param name="dateTo">Filter by created date; null if you want to get all records</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="totalRecords">Total records</param>
        /// <returns>Blog posts</returns>
        public static List<BlogPost> GetAllBlogPosts(int languageId,
                                                     DateTime? dateFrom, DateTime? dateTo, int pageSize,
                                                     int pageIndex, out int totalRecords)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageSize == Int32.MaxValue)
                pageSize = Int32.MaxValue - 1;
            if (pageIndex < 0)
                pageIndex = 0;
            if (pageIndex == Int32.MaxValue)
                pageIndex = Int32.MaxValue - 1;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            List<BlogPost> blogPosts = context.Sp_BlogPostLoadAll(languageId,
                                                                  dateFrom, dateTo, pageSize, pageIndex,
                                                                  out totalRecords).ToList();

            return blogPosts;
        }

        /// <summary>
        /// Gets all blog posts
        /// </summary>
        /// <param name="languageId">Language identifier. 0 if you want to get all news</param>
        /// <param name="tag">Tag</param>
        /// <returns>Blog posts</returns>
        public static List<BlogPost> GetAllBlogPostsByTag(int languageId, string tag)
        {
            tag = tag.Trim();

            List<BlogPost> blogPostsAll = GetAllBlogPosts(languageId);
            var blogPosts = new List<BlogPost>();
            foreach (BlogPost blogPost in blogPostsAll)
            {
                string[] tags = blogPost.ParsedTags;
                if (
                    !String.IsNullOrEmpty(
                        tags.FirstOrDefault(t => t.Equals(tag, StringComparison.InvariantCultureIgnoreCase))))
                {
                    blogPosts.Add(blogPost);
                }
            }

            return blogPosts;
        }

        /// <summary>
        /// Gets all blog post tags
        /// </summary>
        /// <param name="languageId">Language identifier. 0 if you want to get all news</param>
        /// <returns>Blog post tags</returns>
        public static List<BlogPostTag> GetAllBlogPostTags(int languageId)
        {
            var blogPostTags = new List<BlogPostTag>();

            List<BlogPost> blogPostsAll = GetAllBlogPosts(languageId);
            foreach (BlogPost blogPost in blogPostsAll)
            {
                string[] tags = blogPost.ParsedTags;
                foreach (string tag in tags)
                {
                    BlogPostTag foundBlogPostTag =
                        blogPostTags.Find(bpt => bpt.Name.Equals(tag, StringComparison.InvariantCultureIgnoreCase));
                    if (foundBlogPostTag == null)
                    {
                        foundBlogPostTag = new BlogPostTag
                                               {
                                                   Name = tag,
                                                   BlogPostCount = 1
                                               };
                        blogPostTags.Add(foundBlogPostTag);
                    }
                    else
                    {
                        foundBlogPostTag.BlogPostCount++;
                    }
                }
            }

            return blogPostTags;
        }

        /// <summary>
        /// Inserts an blog post
        /// </summary>
        /// <param name="languageId">The language identifier</param>
        /// <param name="blogPostTitle">The blog post title</param>
        /// <param name="blogPostBody">The blog post title</param>
        /// <param name="blogPostAllowComments">A value indicating whether the blog post comments are allowed</param>
        /// <param name="tags">The blog post tags</param>
        /// <param name="createdById">The user identifier who created the blog post</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <returns>Blog post</returns>
        public static BlogPost InsertBlogPost(int languageId, string blogPostTitle,
                                              string blogPostBody, bool blogPostAllowComments,
                                              string tags, int createdById, DateTime createdOn)
        {
            blogPostTitle = CommonHelper.EnsureMaximumLength(blogPostTitle, 200);
            tags = CommonHelper.EnsureMaximumLength(tags, 4000);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            BlogPost blogPost = context.BlogPosts.CreateObject();
            blogPost.LanguageId = languageId;
            blogPost.BlogPostTitle = blogPostTitle;
            blogPost.BlogPostBody = blogPostBody;
            blogPost.BlogPostAllowComments = blogPostAllowComments;
            blogPost.Tags = tags;
            blogPost.CreatedById = createdById;
            blogPost.CreatedOn = createdOn;

            context.BlogPosts.AddObject(blogPost);
            context.SaveChanges();

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(BLOGPOST_PATTERN_KEY);
            }

            return blogPost;
        }

        /// <summary>
        /// Updates the blog post
        /// </summary>
        /// <param name="blogPostId">The blog post identifier</param>
        /// <param name="languageId">The language identifier</param>
        /// <param name="blogPostTitle">The blog post title</param>
        /// <param name="blogPostBody">The blog post title</param>
        /// <param name="blogPostAllowComments">A value indicating whether the blog post comments are allowed</param>
        /// <param name="tags">The blog post tags</param>
        /// <param name="createdById">The user identifier who created the blog post</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <returns>Blog post</returns>
        public static BlogPost UpdateBlogPost(int blogPostId,
                                              int languageId, string blogPostTitle,
                                              string blogPostBody, bool blogPostAllowComments,
                                              string tags, int createdById, DateTime createdOn)
        {
            blogPostTitle = CommonHelper.EnsureMaximumLength(blogPostTitle, 200);
            tags = CommonHelper.EnsureMaximumLength(tags, 4000);

            BlogPost blogPost = GetBlogPostById(blogPostId);
            if (blogPost == null)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(blogPost))
                context.BlogPosts.Attach(blogPost);

            blogPost.LanguageId = languageId;
            blogPost.BlogPostTitle = blogPostTitle;
            blogPost.BlogPostBody = blogPostBody;
            blogPost.BlogPostAllowComments = blogPostAllowComments;
            blogPost.Tags = tags;
            blogPost.CreatedById = createdById;
            blogPost.CreatedOn = createdOn;
            context.SaveChanges();

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(BLOGPOST_PATTERN_KEY);
            }

            return blogPost;
        }

        /// <summary>
        /// Deletes an blog comment
        /// </summary>
        /// <param name="blogCommentId">Blog comment identifier</param>
        public static void DeleteBlogComment(int blogCommentId)
        {
            BlogComment blogComment = GetBlogCommentById(blogCommentId);
            if (blogComment == null)
                return;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(blogComment))
                context.BlogComments.Attach(blogComment);
            context.DeleteObject(blogComment);
            context.SaveChanges();
        }

        /// <summary>
        /// Gets an blog comment
        /// </summary>
        /// <param name="blogCommentId">Blog comment identifier</param>
        /// <returns>A blog comment</returns>
        public static BlogComment GetBlogCommentById(int blogCommentId)
        {
            if (blogCommentId == 0)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<BlogComment> query = from bc in context.BlogComments
                                            where bc.BlogCommentId == blogCommentId
                                            select bc;
            BlogComment blogComment = query.SingleOrDefault();
            return blogComment;
        }

        /// <summary>
        /// Gets a collection of blog comments by blog post identifier
        /// </summary>
        /// <param name="blogPostId">Blog post identifier</param>
        /// <returns>A collection of blog comments</returns>
        public static List<BlogComment> GetBlogCommentsByBlogPostId(int blogPostId)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<BlogComment> query = from bc in context.BlogComments
                                            orderby bc.CreatedOn
                                            where bc.BlogPostId == blogPostId
                                            select bc;
            List<BlogComment> collection = query.ToList();
            return collection;
        }

        /// <summary>
        /// Gets all blog comments
        /// </summary>
        /// <returns>Blog comments</returns>
        public static List<BlogComment> GetAllBlogComments()
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IOrderedQueryable<BlogComment> query = from bc in context.BlogComments
                                                   orderby bc.CreatedOn
                                                   select bc;
            List<BlogComment> collection = query.ToList();
            return collection;
        }

        /// <summary>
        /// Inserts a blog comment
        /// </summary>
        /// <param name="blogPostId">The blog post identifier</param>
        /// <param name="customerId">The customer identifier who commented the blog post</param>
        /// <param name="commentText">The comment text</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <returns>Blog comment</returns>
        public static BlogComment InsertBlogComment(int blogPostId,
                                                    int customerId, string commentText, DateTime createdOn)
        {
            return InsertBlogComment(blogPostId, customerId, commentText,
                                     createdOn, NotifyAboutNewBlogComments);
        }

        /// <summary>
        /// Inserts a blog comment
        /// </summary>
        /// <param name="blogPostId">The blog post identifier</param>
        /// <param name="customerId">The customer identifier who commented the blog post</param>
        /// <param name="commentText">The comment text</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <param name="notify">A value indicating whether to notify the store owner</param>
        /// <returns>Blog comment</returns>
        public static BlogComment InsertBlogComment(int blogPostId,
                                                    int customerId, string commentText, DateTime createdOn, bool notify)
        {
            string IPAddress = NopContext.Current.UserHostAddress;
            return InsertBlogComment(blogPostId, customerId, IPAddress, commentText, createdOn, notify);
        }

        /// <summary>
        /// Inserts a blog comment
        /// </summary>
        /// <param name="blogPostId">The blog post identifier</param>
        /// <param name="customerId">The customer identifier who commented the blog post</param>
        /// <param name="ipAddress">The IP address</param>
        /// <param name="commentText">The comment text</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <param name="notify">A value indicating whether to notify the store owner</param>
        /// <returns>Blog comment</returns>
        public static BlogComment InsertBlogComment(int blogPostId,
                                                    int customerId, string ipAddress, string commentText,
                                                    DateTime createdOn, bool notify)
        {
            ipAddress = CommonHelper.EnsureMaximumLength(ipAddress, 100);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            BlogComment blogComment = context.BlogComments.CreateObject();
            blogComment.BlogPostId = blogPostId;
            blogComment.CustomerId = customerId;
            blogComment.IPAddress = ipAddress;
            blogComment.CommentText = commentText;
            blogComment.CreatedOn = createdOn;

            context.BlogComments.AddObject(blogComment);
            context.SaveChanges();

            if (notify)
            {
                MessageManager.SendBlogCommentNotificationMessage(blogComment,
                                                                  LocalizationManager.DefaultAdminLanguage.LanguageId);
            }

            return blogComment;
        }

        /// <summary>
        /// Updates the blog comment
        /// </summary>
        /// <param name="blogCommentId">The blog comment identifier</param>
        /// <param name="blogPostId">The blog post identifier</param>
        /// <param name="customerId">The customer identifier who commented the blog post</param>
        /// <param name="ipAddress">The IP address</param>
        /// <param name="commentText">The comment text</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <returns>Blog comment</returns>
        public static BlogComment UpdateBlogComment(int blogCommentId, int blogPostId,
                                                    int customerId, string ipAddress, string commentText,
                                                    DateTime createdOn)
        {
            ipAddress = CommonHelper.EnsureMaximumLength(ipAddress, 100);

            BlogComment blogComment = GetBlogCommentById(blogCommentId);
            if (blogComment == null)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(blogComment))
                context.BlogComments.Attach(blogComment);

            blogComment.BlogPostId = blogPostId;
            blogComment.CustomerId = customerId;
            blogComment.IPAddress = ipAddress;
            blogComment.CommentText = commentText;
            blogComment.CreatedOn = createdOn;
            context.SaveChanges();
            return blogComment;
        }

        /// <summary>
        /// Formats the text
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Formatted text</returns>
        public static string FormatCommentText(string text)
        {
            if (String.IsNullOrEmpty(text))
                return string.Empty;

            text = HtmlHelper.FormatText(text, false, true, false, false, false, false);
            return text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether cache is enabled
        /// </summary>
        public static bool CacheEnabled
        {
            get { return SettingManager.GetSettingValueBoolean("Cache.BlogManager.CacheEnabled"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether blog is enabled
        /// </summary>
        public static bool BlogEnabled
        {
            get { return SettingManager.GetSettingValueBoolean("Common.EnableBlog"); }
            set { SettingManager.SetParam("Common.EnableBlog", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the page size for posts
        /// </summary>
        public static int PostsPageSize
        {
            get { return SettingManager.GetSettingValueInteger("Blog.PostsPageSize", 10); }
            set { SettingManager.SetParam("Blog.PostsPageSize", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether not registered user can leave comments
        /// </summary>
        public static bool AllowNotRegisteredUsersToLeaveComments
        {
            get { return SettingManager.GetSettingValueBoolean("Blog.AllowNotRegisteredUsersToLeaveComments"); }
            set { SettingManager.SetParam("Blog.AllowNotRegisteredUsersToLeaveComments", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to notify about new blog comments
        /// </summary>
        public static bool NotifyAboutNewBlogComments
        {
            get { return SettingManager.GetSettingValueBoolean("Blog.NotifyAboutNewBlogComments"); }
            set { SettingManager.SetParam("Blog.NotifyAboutNewBlogComments", value.ToString()); }
        }

        #endregion
    }
}