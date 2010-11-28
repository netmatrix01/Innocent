namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Represents a customer report by attribute key line
    /// </summary>
    public class CustomerReportByAttributeKeyLine : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the attribute key
        /// </summary>
        public string AttributeKey { get; set; }

        /// <summary>
        /// Gets or sets the customer count
        /// </summary>
        public int CustomerCount { get; set; }

        #endregion
    }
}