using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using NopSolutions.NopCommerce.BusinessLogic.Data;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.BusinessLogic.Audit
{
    /// <summary>
    /// Log manager
    /// </summary>
    public class LogManager
    {
        #region Methods

        /// <summary>
        /// Deletes a log item
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        public static void DeleteLog(int logId)
        {
            Log log = GetLogById(logId);
            if (log == null)
                return;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(log))
                context.Log.Attach(log);
            context.DeleteObject(log);
            context.SaveChanges();
        }

        /// <summary>
        /// Clears a log
        /// </summary>
        public static void ClearLog()
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            context.Sp_LogClear();
        }

        /// <summary>
        /// Gets all log items
        /// </summary>
        /// <returns>Log item collection</returns>
        public static List<Log> GetAllLogs()
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IOrderedQueryable<Log> query = from l in context.Log
                                           orderby l.CreatedOn descending
                                           select l;
            List<Log> collection = query.ToList();
            return collection;
        }

        /// <summary>
        /// Gets a log item
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        /// <returns>Log item</returns>
        public static Log GetLogById(int logId)
        {
            if (logId == 0)
                return null;
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<Log> query = from l in context.Log
                                    where l.LogId == logId
                                    select l;
            Log log = query.SingleOrDefault();
            return log;
        }

        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logType">Log item type</param>
        /// <param name="message">The short message</param>
        /// <param name="exception">The exception</param>
        /// <returns>A log item</returns>
        public static Log InsertLog(LogTypeEnum logType, string message, string exception)
        {
            return InsertLog(logType, message, new Exception(String.IsNullOrEmpty(exception) ? string.Empty : exception));
        }

        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logType">Log item type</param>
        /// <param name="message">The short message</param>
        /// <param name="exception">The exception</param>
        /// <returns>A log item</returns>
        public static Log InsertLog(LogTypeEnum logType, string message, Exception exception)
        {
            int customerId = 0;
            if (NopContext.Current != null && NopContext.Current.User != null)
                customerId = NopContext.Current.User.CustomerId;
            string IPAddress = NopContext.Current.UserHostAddress;
            string pageUrl = CommonHelper.GetThisPageUrl(true);

            return InsertLog(logType, 11, message, exception, IPAddress, customerId, pageUrl);
        }

        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logType">Log item type</param>
        /// <param name="severity">The severity</param>
        /// <param name="message">The short message</param>
        /// <param name="exception">The full exception</param>
        /// <param name="IPAddress">The IP address</param>
        /// <param name="customerId">The customer identifier</param>
        /// <param name="pageUrl">The page URL</param>
        /// <returns>Log item</returns>
        public static Log InsertLog(LogTypeEnum logType, int severity, string message,
                                    Exception exception, string IPAddress,
                                    int customerId, string pageUrl)
        {
            //don't log thread abort exception
            if ((exception != null) && (exception is ThreadAbortException))
                return null;

            if (message == null)
                message = string.Empty;

            if (IPAddress == null)
                IPAddress = string.Empty;

            string exceptionStr = exception == null ? string.Empty : exception.ToString();

            string referrerUrl = string.Empty;
            if (HttpContext.Current != null &&
                HttpContext.Current.Request != null &&
                HttpContext.Current.Request.UrlReferrer != null)
                referrerUrl = HttpContext.Current.Request.UrlReferrer.ToString();
            if (referrerUrl == null)
                referrerUrl = string.Empty;

            message = CommonHelper.EnsureMaximumLength(message, 1000);
            exceptionStr = CommonHelper.EnsureMaximumLength(exceptionStr, 4000);
            IPAddress = CommonHelper.EnsureMaximumLength(IPAddress, 100);
            pageUrl = CommonHelper.EnsureMaximumLength(pageUrl, 100);
            referrerUrl = CommonHelper.EnsureMaximumLength(referrerUrl, 100);

            DateTime createdOn = DateTime.UtcNow;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            Log log = context.Log.CreateObject();
            log.LogTypeId = (int) logType;
            log.Severity = severity;
            log.Message = message;
            log.Exception = exceptionStr;
            log.IPAddress = IPAddress;
            log.CustomerId = customerId;
            log.PageUrl = pageUrl;
            log.ReferrerUrl = referrerUrl;
            log.CreatedOn = createdOn;

            context.Log.AddObject(log);
            context.SaveChanges();

            return log;
        }

        #endregion
    }
}