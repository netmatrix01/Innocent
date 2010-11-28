using System;
using System.Collections.Generic;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Forums
{
    /// <summary>
    /// Represents a forum group
    /// </summary>
    public class ForumGroup : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the forum group identifier
        /// </summary>
        public int ForumGroupId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the forums
        /// </summary>
        public List<Forum> Forums
        {
            get { return ForumManager.GetAllForumsByGroupId(ForumGroupId); }
        }

        #endregion
    }
}