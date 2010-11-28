using System;
using System.Collections.Generic;
using System.Linq;

namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Find an address
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="email">Email</param>
        /// <param name="faxNumber">Fax number</param>
        /// <param name="company">Company</param>
        /// <param name="address1">Address 1</param>
        /// <param name="address2">Address 2</param>
        /// <param name="city">City</param>
        /// <param name="stateProvinceId">State/province identifier</param>
        /// <param name="zipPostalCode">Zip postal code</param>
        /// <param name="countryId">Country identifier</param>
        /// <returns>Address</returns>
        public static Address FindAddress(this List<Address> source,
                                          string firstName, string lastName, string phoneNumber,
                                          string email, string faxNumber, string company, string address1,
                                          string address2, string city, int stateProvinceId,
                                          string zipPostalCode, int countryId)
        {
            return source.Find((a) => a.FirstName == firstName &&
                                      a.LastName == lastName &&
                                      a.PhoneNumber == phoneNumber &&
                                      a.Email == email &&
                                      a.FaxNumber == faxNumber &&
                                      a.Company == company &&
                                      a.Address1 == address1 &&
                                      a.Address2 == address2 &&
                                      a.City == city &&
                                      a.StateProvinceId == stateProvinceId &&
                                      a.ZipPostalCode == zipPostalCode &&
                                      a.CountryId == countryId);
        }

        /// <summary>
        /// Returns a customer attribute that has the specified attribute value
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="key">Customer attribute key</param>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A customer attribute that has the specified attribute value; otherwise null</returns>
        public static CustomerAttribute FindAttribute(this List<CustomerAttribute> source,
                                                      string key, int customerId)
        {
            return source.FirstOrDefault(customerAttribute => customerAttribute.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase) && customerAttribute.CustomerId == customerId);
        }
    }
}