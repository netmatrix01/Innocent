using System;
using System.Collections.Generic;
using NopSolutions.NopCommerce.BusinessLogic.Directory;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Polls
{
    /// <summary>
    /// Represents a poll
    /// </summary>
    public class Poll : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the poll identifier
        /// </summary>
        public int PollId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the system keyword
        /// </summary>
        public string SystemKeyword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity should be shown on home page
        /// </summary>
        public bool ShowOnHomePage { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the poll start date and time
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the poll end date and time
        /// </summary>
        public DateTime? EndDate { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the language
        /// </summary>
        public Language Language
        {
            get { return LanguageManager.GetLanguageById(LanguageId); }
        }

        /// <summary>
        /// Gets the poll total vote record count
        /// </summary>
        public int TotalVotes
        {
            get
            {
                int result = 0;
                List<PollAnswer> pollAnswers = PollAnswers;
                foreach (PollAnswer pollAnswer in pollAnswers)
                    result += pollAnswer.Count;
                return result;
            }
        }

        /// <summary>
        /// Gets the poll answers
        /// </summary>
        public List<PollAnswer> PollAnswers
        {
            get { return PollManager.GetPollAnswersByPollId(PollId); }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the poll answers
        /// </summary>
        public virtual ICollection<PollAnswer> NpPollAnswers { get; set; }

        #endregion
    }
}