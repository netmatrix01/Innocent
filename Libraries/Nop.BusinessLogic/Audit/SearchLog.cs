using System;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.Audit
{
    /// <summary>
    /// Represents a search log
    /// </summary>
    public class SearchLog : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the search log identifier
        /// </summary>
        public int SearchLogId { get; set; }

        /// <summary>
        /// Gets or sets the search term
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the customer
        /// </summary>
        public Customer Customer
        {
            get { return CustomerManager.GetCustomerById(CustomerId); }
        }

        #endregion
    }
}