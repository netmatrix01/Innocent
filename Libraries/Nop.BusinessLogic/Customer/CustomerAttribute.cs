namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Represents a customer attribute
    /// </summary>
    public class CustomerAttribute : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the customer attribute identifier
        /// </summary>
        public int CustomerAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the customer
        /// </summary>
        public virtual Customer NpCustomer { get; set; }

        #endregion
    }
}