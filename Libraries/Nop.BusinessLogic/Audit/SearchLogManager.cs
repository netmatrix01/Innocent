using System;
using System.Collections.Generic;
using System.Linq;
using NopSolutions.NopCommerce.BusinessLogic.Data;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.BusinessLogic.Audit
{
    /// <summary>
    /// Search log manager
    /// </summary>
    public class SearchLogManager
    {
        #region Methods

        /// <summary>
        /// Get search term stats
        /// </summary>
        /// <param name="startTime">Start time; null to load all</param>
        /// <param name="endTime">End time; null to load all</param>
        /// <param name="count">Item count. 0 if you want to get all items</param>
        /// <returns>Result</returns>
        public static List<SearchTermReportLine> SearchTermReport(DateTime? startTime,
                                                                  DateTime? endTime, int count)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            List<SearchTermReportLine> report = context.Sp_SearchTermReport(startTime,
                                                                            endTime, count);
            return report;
        }

        /// <summary>
        /// Gets all search log items
        /// </summary>
        /// <returns>Search log collection</returns>
        public static List<SearchLog> GetAllSearchLogs()
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IOrderedQueryable<SearchLog> query = from s in context.SearchLog
                                                 orderby s.CreatedOn descending
                                                 select s;
            List<SearchLog> searchLog = query.ToList();

            return searchLog;
        }

        /// <summary>
        /// Gets a search log item
        /// </summary>
        /// <param name="searchLogId">The search log item identifier</param>
        /// <returns>Search log item</returns>
        public static SearchLog GetSearchLogById(int searchLogId)
        {
            if (searchLogId == 0)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<SearchLog> query = from s in context.SearchLog
                                          where s.SearchLogId == searchLogId
                                          select s;
            SearchLog searchLog = query.SingleOrDefault();
            return searchLog;
        }

        /// <summary>
        /// Inserts a search log item
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        /// <param name="customerId">The customer identifier</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <returns>Search log item</returns>
        public static SearchLog InsertSearchLog(string searchTerm,
                                                int customerId, DateTime createdOn)
        {
            searchTerm = CommonHelper.EnsureMaximumLength(searchTerm, 100);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            SearchLog searchLog = context.SearchLog.CreateObject();
            searchLog.SearchTerm = searchTerm;
            searchLog.CustomerId = customerId;
            searchLog.CreatedOn = createdOn;

            context.SearchLog.AddObject(searchLog);
            context.SaveChanges();
            return searchLog;
        }

        /// <summary>
        /// Clear search log
        /// </summary>
        public static void ClearSearchLog()
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            context.Sp_SearchLogClear();
        }

        #endregion
    }
}