namespace NopSolutions.NopCommerce.BusinessLogic.Audit
{
    /// <summary>
    /// Represents a search term report line
    /// </summary>
    public class SearchTermReportLine : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the search term
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// Gets or sets the search count
        /// </summary>
        public int SearchCount { get; set; }

        #endregion
    }
}