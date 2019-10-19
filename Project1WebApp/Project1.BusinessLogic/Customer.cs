using System;
using System.Text.RegularExpressions;

namespace Project1.BusinessLogic
{
    public class Customer
    {
        private string firstName;
        private string lastName;
        private string street;
        private string city;
        private States state;
        private int zipcode;
        private int custId;
        public string FirstName
        {
            get => firstName;
            set
            {
                try
                {
                    validate(value);
                    firstName = value;
                }
                catch (CustomerException)
                {
                    throw new CustomerException("Invalid First Name");
                }
            }
        }

        private void validate(string value)
        {
            if (!Regex.Match(value, @"\s*[A-z]+\s*?([A-z]\s*)").Success) throw new CustomerException("Empty Input");
        }

        public string LastName {
            get => lastName;
            set
            {
                try
                {
                    validate(value);
                    lastName = value;
                }
                catch (CustomerException)
                {
                    throw new CustomerException("Invalid Last Name");
                }
            }
        }

        public string Street
        {
            get => street;
            set {
                if (value == null) throw new InvalidAddressException("Invalid Street");
                else if (Regex.Match(value, @"\s*\d+\s[A-z0-9]+\s*?(\s[A-z])*").Success) street = value;
                else
                {
                    throw new InvalidAddressException("Invalid Street");
                }
            } }
        public string City {
            get
            {
                return city;
            }

            set
            {
                if (value == null) throw new InvalidAddressException("Invalid City");
                else if (Regex.Match(value, @"\s*[A-z]+\s*?(\s[A-z])*").Success) city = value;
                else { throw new InvalidAddressException("Invalid City"); }
            }
        }

        public States State { get; set; }

        public int Zipcode
        {
            get => zipcode;
            set
            {
                if (Regex.Match(value.ToString(), @"\d{5}").Success) zipcode = value;
                else { throw new InvalidAddressException("Invalid Zipcode"); }
            }
        }
        public int CustID { get; set; }
    }
}