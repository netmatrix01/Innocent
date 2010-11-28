namespace NopSolutions.NopCommerce.BusinessLogic.Audit
{
    /// <summary>
    /// Represents a log item type (need to be synchronize with [Nop_LogType] table)
    /// </summary>
    public enum LogTypeEnum
    {
        /// <summary>
        /// Unknown log item type
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Customer error log item type
        /// </summary>
        CustomerError = 1,
        /// <summary>
        /// Mail error log item type
        /// </summary>
        MailError = 2,
        /// <summary>
        /// Order error log item type
        /// </summary>
        OrderError = 3,
        /// <summary>
        /// Administration area log item type
        /// </summary>
        AdministrationArea = 4,
        /// <summary>
        /// Common error log item type
        /// </summary>
        CommonError = 5,
        /// <summary>
        /// Shipping error log item type
        /// </summary>
        ShippingError = 6,
        /// <summary>
        /// Tax error log item type
        /// </summary>
        TaxError = 7,
    }
}