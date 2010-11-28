using System.Collections.Generic;

namespace NopSolutions.NopCommerce.BusinessLogic.Audit
{
    /// <summary>
    /// Represents an activity log type record
    /// </summary>
    public class ActivityLogType : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the activity log type identifier
        /// </summary>
        public int ActivityLogTypeId { get; set; }

        /// <summary>
        /// Gets or sets the system keyword
        /// </summary>
        public string SystemKeyword { get; set; }

        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the activity log type is enabled
        /// </summary>
        public bool Enabled { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the activity log
        /// </summary>
        public virtual ICollection<ActivityLog> NpActivityLog { get; set; }

        #endregion
    }
}