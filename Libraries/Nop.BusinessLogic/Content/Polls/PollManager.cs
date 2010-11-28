using System;
using System.Collections.Generic;
using System.Linq;
using NopSolutions.NopCommerce.BusinessLogic.Caching;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Data;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Polls
{
    /// <summary>
    /// Poll manager
    /// </summary>
    public class PollManager
    {
        #region Constants

        private const string POLLS_BY_ID_KEY = "Nop.polls.id-{0}";
        private const string POLLANSWERS_BY_POLLID_KEY = "Nop.pollanswers.pollid-{0}";
        private const string POLLS_PATTERN_KEY = "Nop.polls.";
        private const string POLLANSWERS_PATTERN_KEY = "Nop.pollanswers.";

        #endregion

        #region Methods

        /// <summary>
        /// Gets a poll
        /// </summary>
        /// <param name="pollId">The poll identifier</param>
        /// <returns>Poll</returns>
        public static Poll GetPollById(int pollId)
        {
            if (pollId == 0)
                return null;

            string key = string.Format(POLLS_BY_ID_KEY, pollId);
            object obj2 = NopRequestCache.Get(key);
            if (CacheEnabled && (obj2 != null))
            {
                return (Poll) obj2;
            }

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<Poll> query = from p in context.Polls
                                     where p.PollId == pollId
                                     select p;
            Poll poll = query.SingleOrDefault();

            if (CacheEnabled)
            {
                NopRequestCache.Add(key, poll);
            }
            return poll;
        }

        /// <summary>
        /// Gets a poll
        /// </summary>
        /// <param name="systemKeyword">The poll system keyword</param>
        /// <returns>Poll</returns>
        public static Poll GetPollBySystemKeyword(string systemKeyword)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<Poll> query = from p in context.Polls
                                     where p.SystemKeyword == systemKeyword
                                     select p;
            Poll poll = query.FirstOrDefault();

            return poll;
        }

        /// <summary>
        /// Gets all polls
        /// </summary>
        /// <param name="languageId">Language identifier. 0 if you want to get all news</param>
        /// <returns>Poll collection</returns>
        public static List<Poll> GetAllPolls(int languageId)
        {
            return GetPolls(languageId, 0);
        }

        /// <summary>
        /// Gets poll collection
        /// </summary>
        /// <param name="languageId">Language identifier. 0 if you want to get all polls</param>
        /// <param name="pollCount">Poll count to load. 0 if you want to get all polls</param>
        /// <returns>Poll collection</returns>
        public static List<Poll> GetPolls(int languageId, int pollCount)
        {
            return GetPolls(languageId, pollCount, false);
        }

        /// <summary>
        /// Gets poll collection
        /// </summary>
        /// <param name="languageId">Language identifier. 0 if you want to get all polls</param>
        /// <param name="pollCount">Poll count to load. 0 if you want to get all polls</param>
        /// <param name="loadShownOnHomePageOnly">Retrieve only shown on home page polls</param>
        /// <returns>Poll collection</returns>
        public static List<Poll> GetPolls(int languageId, int pollCount, bool loadShownOnHomePageOnly)
        {
            bool showHidden = NopContext.Current.IsAdmin;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            var query = (IQueryable<Poll>) context.Polls;
            if (!showHidden)
            {
                query = query.Where(p => p.Published);
                query = query.Where(p => !p.StartDate.HasValue || p.StartDate <= DateTime.UtcNow);
                query = query.Where(p => !p.EndDate.HasValue || p.EndDate >= DateTime.UtcNow);
            }
            if (loadShownOnHomePageOnly)
            {
                query = query.Where(p => p.ShowOnHomePage);
            }
            if (languageId > 0)
            {
                query = query.Where(p => p.LanguageId == languageId);
            }

            query = query.OrderBy(p => p.DisplayOrder);
            if (pollCount > 0)
            {
                query = query.Take(pollCount);
            }

            List<Poll> polls = query.ToList();

            return polls;
        }

        /// <summary>
        /// Deletes a poll
        /// </summary>
        /// <param name="pollId">The poll identifier</param>
        public static void DeletePoll(int pollId)
        {
            Poll poll = GetPollById(pollId);
            if (poll == null)
                return;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(poll))
                context.Polls.Attach(poll);
            context.DeleteObject(poll);
            context.SaveChanges();

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(POLLS_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(POLLANSWERS_PATTERN_KEY);
            }
        }

        /// <summary>
        /// Inserts a poll
        /// </summary>
        /// <param name="languageId">The language identifier</param>
        /// <param name="name">The name</param>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="published">A value indicating whether the entity is published</param>
        /// <param name="showOnHomePage">A value indicating whether the entity should be shown on home page</param>
        /// <param name="displayOrder">The display order</param>
        /// <param name="startDate">The poll start date and time</param>
        /// <param name="endDate">The poll end date and time</param>
        /// <returns>Poll</returns>
        public static Poll InsertPoll(int languageId, string name, string systemKeyword,
                                      bool published, bool showOnHomePage, int displayOrder, DateTime? startDate,
                                      DateTime? endDate)
        {
            name = CommonHelper.EnsureMaximumLength(name, 400);
            systemKeyword = CommonHelper.EnsureMaximumLength(systemKeyword, 400);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            Poll poll = context.Polls.CreateObject();
            poll.LanguageId = languageId;
            poll.Name = name;
            poll.SystemKeyword = systemKeyword;
            poll.Published = published;
            poll.ShowOnHomePage = showOnHomePage;
            poll.DisplayOrder = displayOrder;
            poll.StartDate = startDate;
            poll.EndDate = endDate;

            context.Polls.AddObject(poll);
            context.SaveChanges();

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(POLLS_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(POLLANSWERS_PATTERN_KEY);
            }

            return poll;
        }

        /// <summary>
        /// Updates the poll
        /// </summary>
        /// <param name="pollId">The poll identifier</param>
        /// <param name="languageId">The language identifier</param>
        /// <param name="name">The name</param>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="published">A value indicating whether the entity is published</param>
        /// <param name="showOnHomePage">A value indicating whether the entity should be shown on home page</param>
        /// <param name="displayOrder">The display order</param>
        /// <param name="startDate">The poll start date and time</param>
        /// <param name="endDate">The poll end date and time</param>
        /// <returns>Poll</returns>
        public static Poll UpdatePoll(int pollId, int languageId, string name,
                                      string systemKeyword, bool published, bool showOnHomePage, int displayOrder,
                                      DateTime? startDate, DateTime? endDate)
        {
            name = CommonHelper.EnsureMaximumLength(name, 400);
            systemKeyword = CommonHelper.EnsureMaximumLength(systemKeyword, 400);

            Poll poll = GetPollById(pollId);
            if (poll == null)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(poll))
                context.Polls.Attach(poll);

            poll.LanguageId = languageId;
            poll.Name = name;
            poll.SystemKeyword = systemKeyword;
            poll.Published = published;
            poll.ShowOnHomePage = showOnHomePage;
            poll.DisplayOrder = displayOrder;
            poll.StartDate = startDate;
            poll.EndDate = endDate;

            context.SaveChanges();

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(POLLS_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(POLLANSWERS_PATTERN_KEY);
            }

            return poll;
        }

        /// <summary>
        /// Is voting record already exists
        /// </summary>
        /// <param name="pollId">Poll identifier</param>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Poll</returns>
        public static bool PollVotingRecordExists(int pollId, int customerId)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<PollVotingRecord> query = from pvr in context.PollVotingRecords
                                                 join pa in context.PollAnswers on pvr.PollAnswerId equals
                                                     pa.PollAnswerId
                                                 where pa.PollId == pollId &&
                                                       pvr.CustomerId == customerId
                                                 select pvr;
            int count = query.Count();
            return count > 0;
        }

        /// <summary>
        /// Gets a poll answer
        /// </summary>
        /// <param name="pollAnswerId">Poll answer identifier</param>
        /// <returns>Poll answer</returns>
        public static PollAnswer GetPollAnswerById(int pollAnswerId)
        {
            if (pollAnswerId == 0)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<PollAnswer> query = from pa in context.PollAnswers
                                           where pa.PollAnswerId == pollAnswerId
                                           select pa;
            PollAnswer pollAnswer = query.SingleOrDefault();
            return pollAnswer;
        }

        /// <summary>
        /// Gets a poll answers by poll identifier
        /// </summary>
        /// <param name="pollId">Poll identifier</param>
        /// <returns>Poll answer collection</returns>
        public static List<PollAnswer> GetPollAnswersByPollId(int pollId)
        {
            string key = string.Format(POLLANSWERS_BY_POLLID_KEY, pollId);
            object obj2 = NopRequestCache.Get(key);
            if (CacheEnabled && (obj2 != null))
            {
                return (List<PollAnswer>) obj2;
            }

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<PollAnswer> query = from pa in context.PollAnswers
                                           orderby pa.DisplayOrder
                                           where pa.PollId == pollId
                                           select pa;
            List<PollAnswer> pollAnswers = query.ToList();

            if (CacheEnabled)
            {
                NopRequestCache.Add(key, pollAnswers);
            }
            return pollAnswers;
        }

        /// <summary>
        /// Deletes a poll answer
        /// </summary>
        /// <param name="pollAnswerId">Poll answer identifier</param>
        public static void DeletePollAnswer(int pollAnswerId)
        {
            PollAnswer pollAnswer = GetPollAnswerById(pollAnswerId);
            if (pollAnswer == null)
                return;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(pollAnswer))
                context.PollAnswers.Attach(pollAnswer);
            context.DeleteObject(pollAnswer);
            context.SaveChanges();

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(POLLS_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(POLLANSWERS_PATTERN_KEY);
            }
        }

        /// <summary>
        /// Inserts a poll answer
        /// </summary>
        /// <param name="pollId">The poll identifier</param>
        /// <param name="name">The poll answer name</param>
        /// <param name="count">The current count</param>
        /// <param name="displayOrder">The display order</param>
        /// <returns>Poll answer</returns>
        public static PollAnswer InsertPollAnswer(int pollId,
                                                  string name, int count, int displayOrder)
        {
            name = CommonHelper.EnsureMaximumLength(name, 400);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            PollAnswer pollAnswer = context.PollAnswers.CreateObject();
            pollAnswer.PollId = pollId;
            pollAnswer.Name = name;
            pollAnswer.Count = count;
            pollAnswer.DisplayOrder = displayOrder;

            context.PollAnswers.AddObject(pollAnswer);
            context.SaveChanges();

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(POLLS_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(POLLANSWERS_PATTERN_KEY);
            }

            return pollAnswer;
        }

        /// <summary>
        /// Updates the poll answer
        /// </summary>
        /// <param name="pollAnswerId">The poll answer identifier</param>
        /// <param name="pollId">The poll identifier</param>
        /// <param name="name">The poll answer name</param>
        /// <param name="count">The current count</param>
        /// <param name="displayOrder">The display order</param>
        /// <returns>Poll answer</returns>
        public static PollAnswer UpdatePollAnswer(int pollAnswerId,
                                                  int pollId, string name, int count, int displayOrder)
        {
            name = CommonHelper.EnsureMaximumLength(name, 400);

            PollAnswer pollAnswer = GetPollAnswerById(pollAnswerId);
            if (pollAnswer == null)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(pollAnswer))
                context.PollAnswers.Attach(pollAnswer);

            pollAnswer.PollId = pollId;
            pollAnswer.Name = name;
            pollAnswer.Count = count;
            pollAnswer.DisplayOrder = displayOrder;
            context.SaveChanges();

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(POLLS_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(POLLANSWERS_PATTERN_KEY);
            }

            return pollAnswer;
        }

        /// <summary>
        /// Creates a poll voting record
        /// </summary>
        /// <param name="pollAnswerId">The poll answer identifier</param>
        /// <param name="customerId">Customer identifer</param>
        public static void CreatePollVotingRecord(int pollAnswerId, int customerId)
        {
            PollAnswer pollAnswer = GetPollAnswerById(pollAnswerId);
            if (pollAnswer == null)
                return;

            //delete previous vote
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            PollVotingRecord oldPvr = (from pvr in context.PollVotingRecords
                                       where pvr.PollAnswerId == pollAnswerId &&
                                             pvr.CustomerId == customerId
                                       select pvr).FirstOrDefault();
            if (oldPvr != null)
            {
                context.DeleteObject(oldPvr);
            }
            context.SaveChanges();

            //insert new vote
            PollVotingRecord newPvr = context.PollVotingRecords.CreateObject();
            newPvr.PollAnswerId = pollAnswerId;
            newPvr.CustomerId = customerId;

            context.PollVotingRecords.AddObject(newPvr);
            context.SaveChanges();

            //new vote records
            int totalVotingRecords = (from pvr in context.PollVotingRecords
                                      where pvr.PollAnswerId == pollAnswerId
                                      select pvr).Count();

            pollAnswer = UpdatePollAnswer(pollAnswer.PollAnswerId,
                                          pollAnswer.PollId,
                                          pollAnswer.Name,
                                          totalVotingRecords,
                                          pollAnswer.DisplayOrder);

            if (CacheEnabled)
            {
                NopRequestCache.RemoveByPattern(POLLS_PATTERN_KEY);
                NopRequestCache.RemoveByPattern(POLLANSWERS_PATTERN_KEY);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether cache is enabled
        /// </summary>
        public static bool CacheEnabled
        {
            get { return SettingManager.GetSettingValueBoolean("Cache.PollManager.CacheEnabled"); }
        }

        #endregion
    }
}