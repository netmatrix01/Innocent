using System.Collections.Generic;
using System.Linq;

namespace NopSolutions.NopCommerce.BusinessLogic.Audit
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Find activity log type by system keyword
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="systemKeyword">The system keyword</param>
        /// <returns>Activity log type item</returns>
        public static ActivityLogType FindBySystemKeyword(this List<ActivityLogType> source,
                                                          string systemKeyword)
        {
            return source.FirstOrDefault(t => t.SystemKeyword.ToLowerInvariant().Equals(systemKeyword.ToLowerInvariant()));
        }
    }
}