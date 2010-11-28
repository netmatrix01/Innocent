using System.Collections.Generic;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Discounts;

namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Represents a customer role
    /// </summary>
    public class CustomerRole : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the customer role identifier
        /// </summary>
        public int CustomerRoleId { get; set; }

        /// <summary>
        /// Gets or sets the customer role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer role is marked as free shiping
        /// </summary>
        public bool FreeShipping { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer role is marked as tax exempt
        /// </summary>
        public bool TaxExempt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer role is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer role has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the customers of the role
        /// </summary>
        public List<Customer> Customers
        {
            get { return CustomerManager.GetCustomersByCustomerRoleId(CustomerRoleId); }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the discounts
        /// </summary>
        public virtual ICollection<Discount> NpDiscounts { get; set; }

        /// <summary>
        /// Gets the customers
        /// </summary>
        public virtual ICollection<Customer> NpCustomers { get; set; }

        #endregion
    }
}