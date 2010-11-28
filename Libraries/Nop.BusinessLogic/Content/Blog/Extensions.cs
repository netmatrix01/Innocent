using System;
using System.Collections.Generic;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Blog
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns all posts published between the two dates.
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="dateFrom">Date from</param>
        /// <param name="dateTo">Date to</param>
        /// <returns>Filtered posts</returns>
        public static List<BlogPost> GetPostsByDate(this List<BlogPost> source,
                                                    DateTime dateFrom, DateTime dateTo)
        {
            List<BlogPost> list =
                source.FindAll(
                    delegate(BlogPost p) { return (dateFrom.Date <= p.CreatedOn && p.CreatedOn.Date <= dateTo); });

            list.TrimExcess();
            return list;
        }
    }
}