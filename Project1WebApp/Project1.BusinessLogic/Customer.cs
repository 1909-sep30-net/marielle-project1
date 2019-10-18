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
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Street { get; set; }
        public string City { get; set; }

        public States State { get; set; }

        public int Zipcode { get; set; }
        public int CustID { get; set; }
    }
}