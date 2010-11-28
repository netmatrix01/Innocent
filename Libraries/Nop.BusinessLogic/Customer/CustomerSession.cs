using System;

namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Represents a customer session
    /// </summary>
    public class CustomerSession : BaseEntity
    {
        #region Fields

        private Customer _customer;

        #endregion

        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the customer session identifier
        /// </summary>
        public Guid CustomerSessionGuid { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the last accessed date and time
        /// </summary>
        public DateTime LastAccessed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer session is expired
        /// </summary>
        public bool IsExpired { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public Customer Customer
        {
            get
            {
                if (_customer == null)
                    _customer = CustomerManager.GetCustomerById(CustomerId);
                return _customer;
            }
        }

        #endregion
    }
}