using System;
using NopSolutions.NopCommerce.BusinessLogic.Directory;

namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Represents an address
    /// </summary>
    public class Address : BaseEntity
    {
        #region Ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the address identifier
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the address is billing or shipping
        /// </summary>
        public bool IsBillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state/province identifier
        /// </summary>
        public int StateProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the zip/postal code
        /// </summary>
        public string ZipPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the customer
        /// </summary>
        public Customer Customer
        {
            get { return CustomerManager.GetCustomerById(CustomerId); }
        }

        /// <summary>
        /// Gets the state/province
        /// </summary>
        public StateProvince StateProvince
        {
            get { return StateProvinceManager.GetStateProvinceById(StateProvinceId); }
        }

        /// <summary>
        /// Gets the country
        /// </summary>
        public Country Country
        {
            get { return CountryManager.GetCountryById(CountryId); }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the customer
        /// </summary>
        public virtual Customer NpCustomer { get; set; }

        #endregion
    }
}