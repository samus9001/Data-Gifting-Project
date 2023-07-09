namespace DataGifting
{
    public class User
    {
        public string firstName 
        { 
            get;
        }
        public string lastName
        {
            get;
        }
        public string emailAddress
        {
            get;
        }
        public string streetName
        {
            get;
        }
        public string houseNumber
        {
            get;
        }
        public string postCode
        {
            get;
        }
        public string city
        {
            get;
        }
        public int phoneNumber
        {
            get;
        }
        public List<int> phoneNumberList
        {
            get;
        }

        public User()
        {
            phoneNumberList = new List<int>();
        }
    }

}