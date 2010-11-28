using System;

namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Represents a customer event arguments
    /// </summary>
    public class CustomerEventArgs : EventArgs
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public Customer Customer { get; set; }

        #endregion
    }
}