using System;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.NewsManagement
{
    /// <summary>
    /// Represents a news comment
    /// </summary>
    public class NewsComment : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the news comment identifier
        /// </summary>
        public int NewsCommentId { get; set; }

        /// <summary>
        /// Gets or sets the news identifier
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the news
        /// </summary>
        public News News
        {
            get { return NewsManager.GetNewsById(NewsId); }
        }

        /// <summary>
        /// Gets the customer
        /// </summary>
        public Customer Customer
        {
            get { return CustomerManager.GetCustomerById(CustomerId); }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the news item
        /// </summary>
        public virtual News NpNews { get; set; }

        #endregion
    }
}