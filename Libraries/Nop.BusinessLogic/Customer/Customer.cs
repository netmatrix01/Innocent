using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Payment;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Affiliates;
using NopSolutions.NopCommerce.BusinessLogic.Promo.Discounts;
using NopSolutions.NopCommerce.BusinessLogic.Shipping;
using NopSolutions.NopCommerce.BusinessLogic.Tax;

namespace NopSolutions.NopCommerce.BusinessLogic.CustomerManagement
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public class Customer : BaseEntity
    {
        #region Fields

        private Address _billingAddressCache;
        private List<CustomerAttribute> _customerAttributesCache;
        private List<CustomerRole> _customerRolesCache;
        private List<RewardPointsHistory> _rewardPointsHistoryCache;
        private Address _shippingAddressCache;

        #endregion

        #region Ctor

        #endregion

        #region Methods

        /// <summary>
        /// Resets cached values for an instance
        /// </summary>
        public void ResetCachedValues()
        {
            _customerAttributesCache = null;
            _customerRolesCache = null;
            _billingAddressCache = null;
            _shippingAddressCache = null;
            _rewardPointsHistoryCache = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public Guid CustomerGuid { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password hash
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the salt key
        /// </summary>
        public string SaltKey { get; set; }

        /// <summary>
        /// Gets or sets the affiliate identifier
        /// </summary>
        public int AffiliateId { get; set; }

        /// <summary>
        /// Gets or sets the billing address identifier
        /// </summary>
        public int BillingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the shipping address identifier
        /// </summary>
        public int ShippingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the last payment method identifier
        /// </summary>
        public int LastPaymentMethodId { get; set; }

        /// <summary>
        /// Gets or sets the last applied coupon code
        /// </summary>
        public string LastAppliedCouponCode { get; set; }

        /// <summary>
        /// Gets or sets the applied gift card coupon code
        /// </summary>
        public string GiftCardCouponCodes { get; set; }

        /// <summary>
        /// Gets or sets the selected checkout attributes
        /// </summary>
        public string CheckoutAttributes { get; set; }

        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the tax display type identifier
        /// </summary>
        public int TaxDisplayTypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is tax exempt
        /// </summary>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is administrator
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is guest
        /// </summary>
        public bool IsGuest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is forum moderator
        /// </summary>
        public bool IsForumModerator { get; set; }

        /// <summary>
        /// Gets or sets the forum post count
        /// </summary>
        public int TotalForumPosts { get; set; }

        /// <summary>
        /// Gets or sets the signature
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of customer registration
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Gets or sets the time zone identifier
        /// </summary>
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Gets or sets the avatar identifier
        /// </summary>
        public int AvatarId { get; set; }

        /// <summary>
        /// Gets or sets the date of birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets the last billing address
        /// </summary>
        public Affiliate Affiliate
        {
            get { return AffiliateManager.GetAffiliateById(AffiliateId); }
        }

        /// <summary>
        /// Gets the customer attributes
        /// </summary>
        public List<CustomerAttribute> CustomerAttributes
        {
            get
            {
                if (_customerAttributesCache == null)
                    _customerAttributesCache = CustomerManager.GetCustomerAttributesByCustomerId(CustomerId);

                return _customerAttributesCache;
            }
        }

        /// <summary>
        /// Gets the customer roles
        /// </summary>
        public List<CustomerRole> CustomerRoles
        {
            get
            {
                if (_customerRolesCache == null)
                    _customerRolesCache = CustomerManager.GetCustomerRolesByCustomerId(CustomerId);

                return _customerRolesCache;
            }
        }

        /// <summary>
        /// Gets the last billing address
        /// </summary>
        public Address BillingAddress
        {
            get
            {
                if (_billingAddressCache == null)
                    _billingAddressCache = CustomerManager.GetAddressById(BillingAddressId);

                return _billingAddressCache;
            }
        }

        /// <summary>
        /// Gets the last shipping address
        /// </summary>
        public Address ShippingAddress
        {
            get
            {
                if (_shippingAddressCache == null)
                    _shippingAddressCache = CustomerManager.GetAddressById(ShippingAddressId);

                return _shippingAddressCache;
            }
        }

        /// <summary>
        /// Gets the language
        /// </summary>
        public Language Language
        {
            get { return LanguageManager.GetLanguageById(LanguageId); }
        }

        /// <summary>
        /// Gets the currency
        /// </summary>
        public Currency Currency
        {
            get { return CurrencyManager.GetCurrencyById(CurrencyId); }
        }

        /// <summary>
        /// Gets the billing addresses
        /// </summary>
        public List<Address> BillingAddresses
        {
            get { return CustomerManager.GetAddressesByCustomerId(CustomerId, true); }
        }

        /// <summary>
        /// Gets the shipping addresses
        /// </summary>
        public List<Address> ShippingAddresses
        {
            get { return CustomerManager.GetAddressesByCustomerId(CustomerId, false); }
        }

        /// <summary>
        /// Gets the orders
        /// </summary>
        public List<Order> Orders
        {
            get { return OrderManager.GetOrdersByCustomerId(CustomerId); }
        }

        /// <summary>
        /// Gets the tax display type
        /// </summary>
        public TaxDisplayTypeEnum TaxDisplayType
        {
            get { return (TaxDisplayTypeEnum) TaxDisplayTypeId; }
            set { TaxDisplayTypeId = (int) value; }
        }

        /// <summary>
        /// Gets the avatar
        /// </summary>
        public Picture Avatar
        {
            get { return PictureManager.GetPictureById(AvatarId); }
        }

        /// <summary>
        /// Gets the last payment method
        /// </summary>
        public PaymentMethod LastPaymentMethod
        {
            get { return PaymentMethodManager.GetPaymentMethodById(LastPaymentMethodId); }
        }

        /// <summary>
        /// Gets or sets the last shipping option
        /// </summary>
        public ShippingOption LastShippingOption
        {
            get
            {
                ShippingOption shippingOption = null;
                CustomerAttribute lastShippingOptionAttr = CustomerAttributes.FindAttribute("LastShippingOption",
                                                                                            CustomerId);
                if (lastShippingOptionAttr != null && !String.IsNullOrEmpty(lastShippingOptionAttr.Value))
                {
                    try
                    {
                        using (TextReader tr = new StringReader(lastShippingOptionAttr.Value))
                        {
                            var xmlS = new XmlSerializer(typeof (ShippingOption));
                            shippingOption = (ShippingOption) xmlS.Deserialize(tr);
                        }
                    }
                    catch
                    {
                        //xml error
                    }
                }
                return shippingOption;
            }
            set
            {
                CustomerAttribute lastShippingOptionAttr = CustomerAttributes.FindAttribute("LastShippingOption",
                                                                                            CustomerId);
                if (value != null)
                {
                    var sb = new StringBuilder();
                    using (TextWriter tw = new StringWriter(sb))
                    {
                        var xmlS = new XmlSerializer(typeof (ShippingOption));
                        xmlS.Serialize(tw, value);
                        string serialized = sb.ToString();
                        if (lastShippingOptionAttr != null)
                            lastShippingOptionAttr =
                                CustomerManager.UpdateCustomerAttribute(lastShippingOptionAttr.CustomerAttributeId,
                                                                        CustomerId, "LastShippingOption", serialized);
                        else
                            lastShippingOptionAttr = CustomerManager.InsertCustomerAttribute(CustomerId,
                                                                                             "LastShippingOption",
                                                                                             serialized);
                    }
                }
                else
                {
                    if (lastShippingOptionAttr != null)
                        CustomerManager.DeleteCustomerAttribute(lastShippingOptionAttr.CustomerAttributeId);
                }

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets the customer full name
        /// </summary>
        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(FirstName))
                    return LastName;
                else
                    return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        ///// <summary>
        ///// Gets or sets the gender
        ///// </summary>
        //public string Gender
        //{
        //    get
        //    {
        //        var customerAttributes = this.CustomerAttributes;
        //        CustomerAttribute genderAttr = customerAttributes.FindAttribute("Gender", this.CustomerId);
        //        if (genderAttr != null)
        //            return genderAttr.Value;
        //        else
        //            return string.Empty;
        //    }
        //    set
        //    {
        //        var customerAttributes = this.CustomerAttributes;
        //        CustomerAttribute genderAttr = customerAttributes.FindAttribute("Gender", this.CustomerId);
        //        if (genderAttr != null)
        //            genderAttr = CustomerManager.UpdateCustomerAttribute(genderAttr.CustomerAttributeId, genderAttr.CustomerId, "Gender", value);
        //        else
        //            genderAttr = CustomerManager.InsertCustomerAttribute(this.CustomerId, "Gender", value);

        //        ResetCachedValues();
        //    }
        //}

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute titleAttr = customerAttributes.FindAttribute("Title", CustomerId);
                if (titleAttr != null)
                    return titleAttr.Value;
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute titleAttr = customerAttributes.FindAttribute("Title", CustomerId);
                if (titleAttr != null)
                    titleAttr = CustomerManager.UpdateCustomerAttribute(titleAttr.CustomerAttributeId,
                                                                        titleAttr.CustomerId, "Title", value);
                else
                    titleAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "Title", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public string FirstName
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute firstNameAttr = customerAttributes.FindAttribute("FirstName", CustomerId);
                if (firstNameAttr != null)
                    return firstNameAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute firstNameAttr = customerAttributes.FindAttribute("FirstName", CustomerId);
                if (firstNameAttr != null)
                    firstNameAttr = CustomerManager.UpdateCustomerAttribute(firstNameAttr.CustomerAttributeId,
                                                                            firstNameAttr.CustomerId, "FirstName", value);
                else
                    firstNameAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "FirstName", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public string LastName
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute lastNameAttr = customerAttributes.FindAttribute("LastName", CustomerId);
                if (lastNameAttr != null)
                    return lastNameAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute lastNameAttr = customerAttributes.FindAttribute("LastName", CustomerId);
                if (lastNameAttr != null)
                    lastNameAttr = CustomerManager.UpdateCustomerAttribute(lastNameAttr.CustomerAttributeId,
                                                                           lastNameAttr.CustomerId, "LastName", value);
                else
                    lastNameAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "LastName", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the company
        /// </summary>
        public string Company
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute companyAttr = customerAttributes.FindAttribute("Company", CustomerId);
                if (companyAttr != null)
                    return companyAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute companyAttr = customerAttributes.FindAttribute("Company", CustomerId);
                if (companyAttr != null)
                    companyAttr = CustomerManager.UpdateCustomerAttribute(companyAttr.CustomerAttributeId,
                                                                          companyAttr.CustomerId, "Company", value);
                else
                    companyAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "Company", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the VAT number (the European Union Value Added Tax)
        /// </summary>
        public string VatNumber
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute vatNumberAttr = customerAttributes.FindAttribute("VatNumber", CustomerId);
                if (vatNumberAttr != null)
                    return vatNumberAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute vatNumberAttr = customerAttributes.FindAttribute("VatNumber", CustomerId);
                if (vatNumberAttr != null)
                    vatNumberAttr = CustomerManager.UpdateCustomerAttribute(vatNumberAttr.CustomerAttributeId,
                                                                            vatNumberAttr.CustomerId, "VatNumber", value);
                else
                    vatNumberAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "VatNumber", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the VAT number status
        /// </summary>
        public VatNumberStatusEnum VatNumberStatus
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute vatNumberStatusAttr = customerAttributes.FindAttribute("VatNumberStatus", CustomerId);
                if (vatNumberStatusAttr != null)
                {
                    int _vatNumberStatusId = 0;
                    int.TryParse(vatNumberStatusAttr.Value, out _vatNumberStatusId);
                    return (VatNumberStatusEnum) _vatNumberStatusId;
                }
                else
                    return VatNumberStatusEnum.Empty;
            }
            set
            {
                var vatNumberStatusId = (int) value;
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute vatNumberStatusAttr = customerAttributes.FindAttribute("VatNumberStatus", CustomerId);
                if (vatNumberStatusAttr != null)
                    vatNumberStatusAttr =
                        CustomerManager.UpdateCustomerAttribute(vatNumberStatusAttr.CustomerAttributeId,
                                                                vatNumberStatusAttr.CustomerId, "VatNumberStatus",
                                                                vatNumberStatusId.ToString());
                else
                    vatNumberStatusAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "VatNumberStatus",
                                                                                  vatNumberStatusId.ToString());

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the street address
        /// </summary>
        public string StreetAddress
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute streetAddressAttr = customerAttributes.FindAttribute("StreetAddress", CustomerId);
                if (streetAddressAttr != null)
                    return streetAddressAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute streetAddressAttr = customerAttributes.FindAttribute("StreetAddress", CustomerId);
                if (streetAddressAttr != null)
                    streetAddressAttr = CustomerManager.UpdateCustomerAttribute(streetAddressAttr.CustomerAttributeId,
                                                                                streetAddressAttr.CustomerId,
                                                                                "StreetAddress", value);
                else
                    streetAddressAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "StreetAddress", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the street address 2
        /// </summary>
        public string StreetAddress2
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute streetAddress2Attr = customerAttributes.FindAttribute("StreetAddress2", CustomerId);
                if (streetAddress2Attr != null)
                    return streetAddress2Attr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute streetAddress2Attr = customerAttributes.FindAttribute("StreetAddress2", CustomerId);
                if (streetAddress2Attr != null)
                    streetAddress2Attr = CustomerManager.UpdateCustomerAttribute(
                        streetAddress2Attr.CustomerAttributeId, streetAddress2Attr.CustomerId, "StreetAddress2", value);
                else
                    streetAddress2Attr = CustomerManager.InsertCustomerAttribute(CustomerId, "StreetAddress2", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the zip/postal code
        /// </summary>
        public string ZipPostalCode
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute zipPostalCodeAttr = customerAttributes.FindAttribute("ZipPostalCode", CustomerId);
                if (zipPostalCodeAttr != null)
                    return zipPostalCodeAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute zipPostalCodeAttr = customerAttributes.FindAttribute("ZipPostalCode", CustomerId);
                if (zipPostalCodeAttr != null)
                    zipPostalCodeAttr = CustomerManager.UpdateCustomerAttribute(zipPostalCodeAttr.CustomerAttributeId,
                                                                                zipPostalCodeAttr.CustomerId,
                                                                                "ZipPostalCode", value);
                else
                    zipPostalCodeAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "ZipPostalCode", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public string City
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute cityAttr = customerAttributes.FindAttribute("City", CustomerId);
                if (cityAttr != null)
                    return cityAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute cityAttr = customerAttributes.FindAttribute("City", CustomerId);
                if (cityAttr != null)
                    cityAttr = CustomerManager.UpdateCustomerAttribute(cityAttr.CustomerAttributeId, cityAttr.CustomerId,
                                                                       "City", value);
                else
                    cityAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "City", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute phoneNumberAttr = customerAttributes.FindAttribute("PhoneNumber", CustomerId);
                if (phoneNumberAttr != null)
                    return phoneNumberAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute phoneNumberAttr = customerAttributes.FindAttribute("PhoneNumber", CustomerId);
                if (phoneNumberAttr != null)
                    phoneNumberAttr = CustomerManager.UpdateCustomerAttribute(phoneNumberAttr.CustomerAttributeId,
                                                                              phoneNumberAttr.CustomerId, "PhoneNumber",
                                                                              value);
                else
                    phoneNumberAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "PhoneNumber", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        public string FaxNumber
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute faxNumberAttr = customerAttributes.FindAttribute("FaxNumber", CustomerId);
                if (faxNumberAttr != null)
                    return faxNumberAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                value = value.Trim();

                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute faxNumberAttr = customerAttributes.FindAttribute("FaxNumber", CustomerId);
                if (faxNumberAttr != null)
                    faxNumberAttr = CustomerManager.UpdateCustomerAttribute(faxNumberAttr.CustomerAttributeId,
                                                                            faxNumberAttr.CustomerId, "FaxNumber", value);
                else
                    faxNumberAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "FaxNumber", value);

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int CountryId
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute countryIdAttr = customerAttributes.FindAttribute("CountryId", CustomerId);
                if (countryIdAttr != null)
                {
                    int _countryId = 0;
                    int.TryParse(countryIdAttr.Value, out _countryId);
                    return _countryId;
                }
                else
                    return 0;
            }
            set
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute countryIdAttr = customerAttributes.FindAttribute("CountryId", CustomerId);
                if (countryIdAttr != null)
                    countryIdAttr = CustomerManager.UpdateCustomerAttribute(countryIdAttr.CustomerAttributeId,
                                                                            countryIdAttr.CustomerId, "CountryId",
                                                                            value.ToString());
                else
                    countryIdAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "CountryId", value.ToString());

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the state/province identifier
        /// </summary>
        public int StateProvinceId
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute stateProvinceIdAttr = customerAttributes.FindAttribute("StateProvinceId", CustomerId);
                if (stateProvinceIdAttr != null)
                {
                    int _stateProvinceId = 0;
                    int.TryParse(stateProvinceIdAttr.Value, out _stateProvinceId);
                    return _stateProvinceId;
                }
                else
                    return 0;
            }
            set
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute stateProvinceIdAttr = customerAttributes.FindAttribute("StateProvinceId", CustomerId);
                if (stateProvinceIdAttr != null)
                    stateProvinceIdAttr =
                        CustomerManager.UpdateCustomerAttribute(stateProvinceIdAttr.CustomerAttributeId,
                                                                stateProvinceIdAttr.CustomerId, "StateProvinceId",
                                                                value.ToString());
                else
                    stateProvinceIdAttr = CustomerManager.InsertCustomerAttribute(CustomerId, "StateProvinceId",
                                                                                  value.ToString());

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets the value indivating whether customer is agree to receive newsletters
        /// </summary>
        public bool ReceiveNewsletter
        {
            get
            {
                NewsLetterSubscription subscription = NewsLetterSubscription;

                return (subscription != null && subscription.Active);
            }
            set
            {
                NewsLetterSubscription subscription = NewsLetterSubscription;
                if (subscription != null)
                {
                    if (value)
                    {
                        MessageManager.UpdateNewsLetterSubscription(subscription.NewsLetterSubscriptionId,
                                                                    subscription.Email, true);
                    }
                    else
                    {
                        MessageManager.DeleteNewsLetterSubscription(subscription.NewsLetterSubscriptionId);
                    }
                }
                else
                {
                    if (value)
                    {
                        MessageManager.InsertNewsLetterSubscription(Email, value);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the NewsLetterSubscription entity if the customer has been subscribed to newsletters
        /// </summary>
        public NewsLetterSubscription NewsLetterSubscription
        {
            get { return MessageManager.GetNewsLetterSubscriptionByEmail(Email); }
        }

        /// <summary>
        /// Gets or sets the password recovery token
        /// </summary>
        public string PasswordRecoveryToken
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute passwordRecoveryAttr = customerAttributes.FindAttribute("PasswordRecoveryToken",
                                                                                          CustomerId);
                if (passwordRecoveryAttr != null)
                    return passwordRecoveryAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute passwordRecoveryAttr = customerAttributes.FindAttribute("PasswordRecoveryToken",
                                                                                          CustomerId);

                if (passwordRecoveryAttr != null)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        passwordRecoveryAttr =
                            CustomerManager.UpdateCustomerAttribute(passwordRecoveryAttr.CustomerAttributeId,
                                                                    passwordRecoveryAttr.CustomerId,
                                                                    "PasswordRecoveryToken", value);
                    }
                    else
                    {
                        CustomerManager.DeleteCustomerAttribute(passwordRecoveryAttr.CustomerAttributeId);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        passwordRecoveryAttr = CustomerManager.InsertCustomerAttribute(CustomerId,
                                                                                       "PasswordRecoveryToken", value);
                    }
                }
                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the account activation token
        /// </summary>
        public string AccountActivationToken
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute accountActivationAttr = customerAttributes.FindAttribute("AccountActivationToken",
                                                                                           CustomerId);
                if (accountActivationAttr != null)
                    return accountActivationAttr.Value;
                else
                    return string.Empty;
            }
            set
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute accountActivationAttr = customerAttributes.FindAttribute("AccountActivationToken",
                                                                                           CustomerId);

                if (accountActivationAttr != null)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        accountActivationAttr =
                            CustomerManager.UpdateCustomerAttribute(accountActivationAttr.CustomerAttributeId,
                                                                    accountActivationAttr.CustomerId,
                                                                    "AccountActivationToken", value);
                    }
                    else
                    {
                        CustomerManager.DeleteCustomerAttribute(accountActivationAttr.CustomerAttributeId);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        accountActivationAttr = CustomerManager.InsertCustomerAttribute(CustomerId,
                                                                                        "AccountActivationToken", value);
                    }
                }
                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets reward points collection
        /// </summary>
        public List<RewardPointsHistory> RewardPointsHistory
        {
            get
            {
                if (CustomerId == 0)
                    return new List<RewardPointsHistory>();
                if (_rewardPointsHistoryCache == null)
                {
                    int totalRecords = 0;
                    _rewardPointsHistoryCache = OrderManager.GetAllRewardPointsHistoryEntries(CustomerId,
                                                                                              null, int.MaxValue, 0,
                                                                                              out totalRecords);
                }
                return _rewardPointsHistoryCache;
            }
        }

        /// <summary>
        /// Gets reward points balance
        /// </summary>
        public int RewardPointsBalance
        {
            get
            {
                int result = 0;
                if (RewardPointsHistory.Count > 0)
                    result = RewardPointsHistory[0].PointsBalance;
                return result;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use reward points during checkout
        /// </summary>
        public bool UseRewardPointsDuringCheckout
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute useRewardPointsAttr = customerAttributes.FindAttribute(
                    "UseRewardPointsDuringCheckout", CustomerId);
                if (useRewardPointsAttr != null)
                {
                    bool _useRewardPoints = false;
                    bool.TryParse(useRewardPointsAttr.Value, out _useRewardPoints);
                    return _useRewardPoints;
                }
                else
                    return false;
            }
            set
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute useRewardPointsAttr = customerAttributes.FindAttribute(
                    "UseRewardPointsDuringCheckout", CustomerId);
                if (useRewardPointsAttr != null)
                    useRewardPointsAttr =
                        CustomerManager.UpdateCustomerAttribute(useRewardPointsAttr.CustomerAttributeId,
                                                                useRewardPointsAttr.CustomerId,
                                                                "UseRewardPointsDuringCheckout", value.ToString());
                else
                    useRewardPointsAttr = CustomerManager.InsertCustomerAttribute(CustomerId,
                                                                                  "UseRewardPointsDuringCheckout",
                                                                                  value.ToString());

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether customer is notified about new private messages
        /// </summary>
        public bool NotifiedAboutNewPrivateMessages
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute attr = customerAttributes.FindAttribute("NotifiedAboutNewPrivateMessages", CustomerId);
                if (attr != null)
                {
                    bool _result = false;
                    bool.TryParse(attr.Value, out _result);
                    return _result;
                }
                else
                    return false;
            }
            set
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute attr = customerAttributes.FindAttribute("NotifiedAboutNewPrivateMessages", CustomerId);
                if (attr != null)
                    attr = CustomerManager.UpdateCustomerAttribute(attr.CustomerAttributeId, attr.CustomerId,
                                                                   "NotifiedAboutNewPrivateMessages", value.ToString());
                else
                    attr = CustomerManager.InsertCustomerAttribute(CustomerId, "NotifiedAboutNewPrivateMessages",
                                                                   value.ToString());

                ResetCachedValues();
            }
        }

        /// <summary>
        /// Gets or sets the impersonated customer identifier
        /// </summary>
        public Guid ImpersonatedCustomerGuid
        {
            get
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute attr = customerAttributes.FindAttribute("ImpersonatedCustomerGuid", CustomerId);
                if (attr != null)
                {
                    Guid _impersonatedCustomerGuid = Guid.Empty;
                    Guid.TryParse(attr.Value, out _impersonatedCustomerGuid);
                    return _impersonatedCustomerGuid;
                }
                else
                    return Guid.Empty;
            }
            set
            {
                List<CustomerAttribute> customerAttributes = CustomerAttributes;
                CustomerAttribute attr = customerAttributes.FindAttribute("ImpersonatedCustomerGuid", CustomerId);
                if (attr != null)
                    attr = CustomerManager.UpdateCustomerAttribute(attr.CustomerAttributeId, attr.CustomerId,
                                                                   "ImpersonatedCustomerGuid", value.ToString());
                else
                    attr = CustomerManager.InsertCustomerAttribute(CustomerId, "ImpersonatedCustomerGuid",
                                                                   value.ToString());

                ResetCachedValues();
            }
        }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets the customer roles
        /// </summary>
        public virtual ICollection<CustomerRole> NpCustomerRoles { get; set; }

        /// <summary>
        /// Gets the discount usage history
        /// </summary>
        public virtual ICollection<DiscountUsageHistory> NpDiscountUsageHistory { get; set; }

        /// <summary>
        /// Gets the gift card usage history
        /// </summary>
        public virtual ICollection<GiftCardUsageHistory> NpGiftCardUsageHistory { get; set; }

        /// <summary>
        /// Gets the reward points usage history
        /// </summary>
        public virtual ICollection<RewardPointsHistory> NpRewardPointsHistory { get; set; }

        #endregion
    }
}