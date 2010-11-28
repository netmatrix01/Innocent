namespace NopSolutions.NopCommerce.BusinessLogic.Content.Polls
{
    /// <summary>
    /// Represents a poll answer
    /// </summary>
    public class PollAnswer : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the poll answer identifier
        /// </summary>
        public int PollAnswerId { get; set; }

        /// <summary>
        /// Gets or sets the poll identifier
        /// </summary>
        public int PollId { get; set; }

        /// <summary>
        /// Gets or sets the poll answer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the current count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the poll
        /// </summary>
        public Poll Poll
        {
            get { return PollManager.GetPollById(PollId); }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the poll
        /// </summary>
        public virtual Poll NpPoll { get; set; }

        #endregion
    }
}