using System;

namespace NopSolutions.NopCommerce.BusinessLogic.Audit.UsersOnline
{
    /// <summary>
    /// Represents an online user info
    /// </summary>
    public class OnlineUserInfo
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid OnlineUserGuid { get; set; }

        /// <summary>
        /// Gets or sets the associated customer identifier (if he exists)
        /// </summary>
        public int? AssociatedCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the IP Address
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the last page visited
        /// </summary>
        public string LastPageVisited { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the last visit date and time
        /// </summary>
        public DateTime LastVisit { get; set; }

        #endregion
    }
}