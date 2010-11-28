using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace NopSolutions.NopCommerce.BusinessLogic.Caching
{
    /// <summary>
    /// Represents a NopRequestCache
    /// </summary>
    public class NopRequestCache
    {
        #region Ctor

        /// <summary>
        /// Creates a new instance of the NopRequestCache class
        /// </summary>
        private NopRequestCache()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new instance of the NopRequestCache class
        /// </summary>
        protected static IDictionary GetItems()
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                return current.Items;
            }

            return null;
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public static object Get(string key)
        {
            IDictionary _items = GetItems();
            if (_items == null)
                return null;

            return _items[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">object</param>
        public static void Add(string key, object obj)
        {
            IDictionary _items = GetItems();
            if (_items == null)
                return;

            if (IsEnabled && (obj != null))
            {
                _items.Add(key, obj);
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            IDictionary _items = GetItems();
            if (_items == null)
                return;

            _items.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public static void RemoveByPattern(string pattern)
        {
            IDictionary _items = GetItems();
            if (_items == null)
                return;

            IDictionaryEnumerator enumerator = _items.GetEnumerator();
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    keysToRemove.Add(enumerator.Key.ToString());
                }
            }

            foreach (string key in keysToRemove)
            {
                _items.Remove(key);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the cache is enabled
        /// </summary>
        public static bool IsEnabled
        {
            get { return true; }
        }

        #endregion
    }
}