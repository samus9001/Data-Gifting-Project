namespace DataGifting
{
    public class User
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private string _emailAddress;

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        private Address _address;

        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private List<SenderPlanDetails> _plan;

        public List<SenderPlanDetails> Plan
        {
            get { return _plan; }
            set { _plan = value; }
        }
    }
}