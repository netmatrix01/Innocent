using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Polls
{
    /// <summary>
    /// Represents a poll voting record
    /// </summary>
    public class PollVotingRecord : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the poll voting record identifier
        /// </summary>
        public int PollVotingRecordId { get; set; }

        /// <summary>
        /// Gets or sets the poll answer identifier
        /// </summary>
        public int PollAnswerId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the poll answer
        /// </summary>
        public virtual PollAnswer NpPollAnswer { get; set; }

        /// <summary>
        /// Gets the customer
        /// </summary>
        public virtual Customer NpCustomer { get; set; }

        #endregion
    }
}