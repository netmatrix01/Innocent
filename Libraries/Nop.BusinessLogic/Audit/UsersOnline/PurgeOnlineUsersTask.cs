using System;
using System.Xml;
using NopSolutions.NopCommerce.BusinessLogic.Tasks;

namespace NopSolutions.NopCommerce.BusinessLogic.Audit.UsersOnline
{
    /// <summary>
    /// Purge onlie users schedueled task implementation
    /// </summary>
    public class PurgeOnlineUsersTask : ITask
    {
        #region ITask Members

        /// <summary>
        /// Executes the clear cache task
        /// </summary>
        /// <param name="node">XML node that represents a task description</param>
        public void Execute(XmlNode node)
        {
            try
            {
                OnlineUserManager.PurgeUsers();
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.CustomerError, "Error purging online users.", ex);
            }
        }

        #endregion
    }
}