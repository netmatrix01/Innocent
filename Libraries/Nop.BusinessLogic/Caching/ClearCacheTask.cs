using System;
using System.Xml;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Tasks;

namespace NopSolutions.NopCommerce.BusinessLogic.Caching
{
    /// <summary>
    /// Clear cache schedueled task implementation
    /// </summary>
    public class ClearCacheTask : ITask
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
                NopStaticCache.Clear();
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.AdministrationArea, "Error clearing cache.", ex);
            }
        }

        #endregion
    }
}