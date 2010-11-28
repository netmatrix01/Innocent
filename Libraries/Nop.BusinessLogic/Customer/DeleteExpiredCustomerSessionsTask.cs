using System;
using System.Xml;
using NopSolutions.NopCommerce.BusinessLogic.Tasks;

namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Represents a task for deleting expired customer sessions
    /// </summary>
    public class DeleteExpiredCustomerSessionsTask : ITask
    {
        private int _deleteExpiredCustomerSessionsOlderThanMinutes = 43200; //30 days

        #region ITask Members

        /// <summary>
        /// Executes a task
        /// </summary>
        /// <param name="node">Xml node that represents a task description</param>
        public void Execute(XmlNode node)
        {
            XmlAttribute attribute1 = node.Attributes["deleteExpiredCustomerSessionsOlderThanMinutes"];
            if (attribute1 != null && !String.IsNullOrEmpty(attribute1.Value))
            {
                _deleteExpiredCustomerSessionsOlderThanMinutes = int.Parse(attribute1.Value);
            }

            DateTime olderThan = DateTime.UtcNow;
            olderThan = olderThan.AddMinutes(-(double) _deleteExpiredCustomerSessionsOlderThanMinutes);
            CustomerManager.DeleteExpiredCustomerSessions(olderThan);
        }

        #endregion
    }
}