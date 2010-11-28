using System;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.Audit
{
    /// <summary>
    /// Represents a log record
    /// </summary>
    public class Log : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the log identifier
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// Gets or sets the log type identifier
        /// </summary>
        public int LogTypeId { get; set; }

        /// <summary>
        /// Gets or sets the severity
        /// </summary>
        public int Severity { get; set; }

        /// <summary>
        /// Gets or sets the short message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the full exception
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the page URL
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets the referrer URL
        /// </summary>
        public string ReferrerUrl { get; set; }

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

        /// <summary>
        /// Gets the log type
        /// </summary>
        public LogTypeEnum LogType
        {
            get { return (LogTypeEnum) LogTypeId; }
        }

        #endregion
    }
}