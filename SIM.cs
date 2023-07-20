namespace DataGifting
{
    public class SIM
    {
        private int _serialNumber;

        public int SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                if (value == 12)
                {
                    _serialNumber = value;
                }
            }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }
    }
}
