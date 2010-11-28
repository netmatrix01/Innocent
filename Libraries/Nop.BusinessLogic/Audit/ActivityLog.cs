using System;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.Audit
{
    /// <summary>
    /// Represents an activity log record
    /// </summary>
    public class ActivityLog : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the activity log identifier
        /// </summary>
        public int ActivityLogId { get; set; }

        /// <summary>
        /// Gets or sets the activity log type identifier
        /// </summary>
        public int ActivityLogTypeId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the activity comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Custom properties

        /// <summary>
        /// Gets the activity log type
        /// </summary>
        public ActivityLogType ActivityLogType
        {
            get { return CustomerActivityManager.GetActivityTypeById(ActivityLogTypeId); }
        }

        /// <summary>
        /// Gers the customer
        /// </summary>
        public Customer Customer
        {
            get { return CustomerManager.GetCustomerById(CustomerId); }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the activity log type
        /// </summary>
        public virtual ActivityLogType NpActivityLogType { get; set; }

        /// <summary>
        /// Gets the customer
        /// </summary>
        public virtual Customer NpCustomer { get; set; }

        #endregion
    }
}