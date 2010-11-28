namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Represents a customer report by language line
    /// </summary>
    public class CustomerReportByLanguageLine : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the language ID
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the customer count
        /// </summary>
        public int CustomerCount { get; set; }

        #endregion
    }
}