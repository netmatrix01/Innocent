using System;
using System.Collections.Generic;
using System.Linq;
using NopSolutions.NopCommerce.BusinessLogic.Data;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.BusinessLogic.Content.Topics
{
    /// <summary>
    /// Message manager
    /// </summary>
    public class TopicManager
    {
        #region Methods

        /// <summary>
        /// Deletes a topic
        /// </summary>
        /// <param name="topicId">Topic identifier</param>
        public static void DeleteTopic(int topicId)
        {
            Topic topic = GetTopicById(topicId);
            if (topic == null)
                return;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(topic))
                context.Topics.Attach(topic);
            context.DeleteObject(topic);
            context.SaveChanges();
        }

        /// <summary>
        /// Inserts a topic
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="isPasswordProtected">The value indicating whether this topic is password proceted</param>
        /// <param name="password">The password to access the content of this topic</param>
        /// <param name="includeInSitemap">The value indicating whether this topic should be included in sitemap</param>
        /// <returns>Topic</returns>
        public static Topic InsertTopic(string name, bool isPasswordProtected,
                                        string password, bool includeInSitemap)
        {
            name = CommonHelper.EnsureMaximumLength(name, 200);
            password = CommonHelper.EnsureMaximumLength(password, 200);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            Topic topic = context.Topics.CreateObject();
            topic.Name = name;
            topic.IsPasswordProtected = isPasswordProtected;
            topic.Password = password;
            topic.IncludeInSitemap = includeInSitemap;

            context.Topics.AddObject(topic);
            context.SaveChanges();

            return topic;
        }

        /// <summary>
        /// Updates the topic
        /// </summary>
        /// <param name="topicId">The topic identifier</param>
        /// <param name="name">The name</param>
        /// <param name="isPasswordProtected">The value indicating whether this topic is password proceted</param>
        /// <param name="password">The password to access the content of this topic</param>
        /// <param name="includeInSitemap">The value indicating whether this topic should be included in sitemap</param>
        /// <returns>Topic</returns>
        public static Topic UpdateTopic(int topicId, string name,
                                        bool isPasswordProtected, string password, bool includeInSitemap)
        {
            name = CommonHelper.EnsureMaximumLength(name, 200);
            password = CommonHelper.EnsureMaximumLength(password, 200);

            Topic topic = GetTopicById(topicId);
            if (topic == null)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(topic))
                context.Topics.Attach(topic);

            topic.Name = name;
            topic.IsPasswordProtected = isPasswordProtected;
            topic.Password = password;
            topic.IncludeInSitemap = includeInSitemap;
            context.SaveChanges();

            return topic;
        }

        /// <summary>
        /// Gets a topic by template identifier
        /// </summary>
        /// <param name="topicId">topic identifier</param>
        /// <returns>topic</returns>
        public static Topic GetTopicById(int topicId)
        {
            if (topicId == 0)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<Topic> query = from t in context.Topics
                                      where t.TopicId == topicId
                                      select t;
            Topic topic = query.SingleOrDefault();

            return topic;
        }

        /// <summary>
        /// Gets all topics
        /// </summary>
        /// <returns>topic collection</returns>
        public static List<Topic> GetAllTopics()
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IOrderedQueryable<Topic> query = from t in context.Topics
                                             orderby t.Name
                                             select t;
            List<Topic> topics = query.ToList();
            return topics;
        }

        /// <summary>
        /// Gets a localized topic by identifier
        /// </summary>
        /// <param name="localizedTopicId">Localized topic identifier</param>
        /// <returns>Localized topic</returns>
        public static LocalizedTopic GetLocalizedTopicById(int localizedTopicId)
        {
            if (localizedTopicId == 0)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<LocalizedTopic> query = from tl in context.LocalizedTopics
                                               where tl.TopicLocalizedId == localizedTopicId
                                               select tl;
            LocalizedTopic localizedTopic = query.SingleOrDefault();
            return localizedTopic;
        }

        /// <summary>
        /// Gets a localized topic by parent topic identifier and language identifier
        /// </summary>
        /// <param name="topicId">The topic identifier</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized topic</returns>
        public static LocalizedTopic GetLocalizedTopic(int topicId, int languageId)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<LocalizedTopic> query = from tl in context.LocalizedTopics
                                               where tl.TopicId == topicId &&
                                                     tl.LanguageId == languageId
                                               select tl;
            LocalizedTopic localizedTopic = query.FirstOrDefault();

            return localizedTopic;
        }

        /// <summary>
        /// Gets a localized topic by name and language identifier
        /// </summary>
        /// <param name="topicName">Topic name</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized topic</returns>
        public static LocalizedTopic GetLocalizedTopic(string topicName, int languageId)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<LocalizedTopic> query = from tl in context.LocalizedTopics
                                               join t in context.Topics on tl.TopicId equals t.TopicId
                                               where tl.LanguageId == languageId &&
                                                     t.Name == topicName
                                               select tl;
            LocalizedTopic localizedTopic = query.FirstOrDefault();

            return localizedTopic;
        }

        /// <summary>
        /// Deletes a localized topic
        /// </summary>
        /// <param name="localizedTopicId">topic identifier</param>
        public static void DeleteLocalizedTopic(int localizedTopicId)
        {
            LocalizedTopic localizedTopic = GetLocalizedTopicById(localizedTopicId);
            if (localizedTopic == null)
                return;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(localizedTopic))
                context.LocalizedTopics.Attach(localizedTopic);
            context.DeleteObject(localizedTopic);
            context.SaveChanges();
        }

        /// <summary>
        /// Gets all localized topics
        /// </summary>
        /// <param name="topicName">topic name</param>
        /// <returns>Localized topic collection</returns>
        public static List<LocalizedTopic> GetAllLocalizedTopics(string topicName)
        {
            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            IQueryable<LocalizedTopic> query = from tl in context.LocalizedTopics
                                               join t in context.Topics on tl.TopicId equals t.TopicId
                                               where t.Name == topicName
                                               orderby tl.LanguageId
                                               select tl;
            List<LocalizedTopic> localizedTopics = query.ToList();
            return localizedTopics;
        }

        /// <summary>
        /// Inserts a localized topic
        /// </summary>
        /// <param name="topicId">The topic identifier</param>
        /// <param name="languageId">The language identifier</param>
        /// <param name="title">The title</param>
        /// <param name="body">The body</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <param name="updatedOn">The date and time of instance update</param>
        /// <param name="metaKeywords">The meta keywords</param>
        /// <param name="metaDescription">The meta description</param>
        /// <param name="metaTitle">The meta title</param>
        /// <returns>Localized topic</returns>
        public static LocalizedTopic InsertLocalizedTopic(int topicId,
                                                          int languageId, string title, string body,
                                                          DateTime createdOn, DateTime updatedOn,
                                                          string metaKeywords, string metaDescription, string metaTitle)
        {
            title = CommonHelper.EnsureMaximumLength(title, 200);
            metaTitle = CommonHelper.EnsureMaximumLength(metaTitle, 400);
            metaKeywords = CommonHelper.EnsureMaximumLength(metaKeywords, 400);
            metaDescription = CommonHelper.EnsureMaximumLength(metaDescription, 4000);

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;

            LocalizedTopic localizedTopic = context.LocalizedTopics.CreateObject();
            localizedTopic.TopicId = topicId;
            localizedTopic.LanguageId = languageId;
            localizedTopic.Title = title;
            localizedTopic.Body = body;
            localizedTopic.CreatedOn = createdOn;
            localizedTopic.UpdatedOn = updatedOn;
            localizedTopic.MetaKeywords = metaKeywords;
            localizedTopic.MetaDescription = metaDescription;
            localizedTopic.MetaTitle = metaTitle;

            context.LocalizedTopics.AddObject(localizedTopic);
            context.SaveChanges();

            return localizedTopic;
        }

        /// <summary>
        /// Updates the localized topic
        /// </summary>
        /// <param name="topicLocalizedId">The localized topic identifier</param>
        /// <param name="topicId">The topic identifier</param>
        /// <param name="languageId">The language identifier</param>
        /// <param name="title">The title</param>
        /// <param name="body">The body</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <param name="updatedOn">The date and time of instance update</param>
        /// <param name="metaKeywords">The meta keywords</param>
        /// <param name="metaDescription">The meta description</param>
        /// <param name="metaTitle">The meta title</param>
        /// <returns>Localized topic</returns>
        public static LocalizedTopic UpdateLocalizedTopic(int topicLocalizedId,
                                                          int topicId, int languageId, string title, string body,
                                                          DateTime createdOn, DateTime updatedOn,
                                                          string metaKeywords, string metaDescription, string metaTitle)
        {
            title = CommonHelper.EnsureMaximumLength(title, 200);
            metaTitle = CommonHelper.EnsureMaximumLength(metaTitle, 400);
            metaKeywords = CommonHelper.EnsureMaximumLength(metaKeywords, 400);
            metaDescription = CommonHelper.EnsureMaximumLength(metaDescription, 4000);

            LocalizedTopic localizedTopic = GetLocalizedTopicById(topicLocalizedId);
            if (localizedTopic == null)
                return null;

            NopObjectContext context = ObjectContextHelper.CurrentObjectContext;
            if (!context.IsAttached(localizedTopic))
                context.LocalizedTopics.Attach(localizedTopic);

            localizedTopic.TopicId = topicId;
            localizedTopic.LanguageId = languageId;
            localizedTopic.Title = title;
            localizedTopic.Body = body;
            localizedTopic.CreatedOn = createdOn;
            localizedTopic.UpdatedOn = updatedOn;
            localizedTopic.MetaKeywords = metaKeywords;
            localizedTopic.MetaDescription = metaDescription;
            localizedTopic.MetaTitle = metaTitle;
            context.SaveChanges();

            return localizedTopic;
        }

        #endregion
    }
}