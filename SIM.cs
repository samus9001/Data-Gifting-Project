namespace DataGifting
{
    public class SIM
    {
        private string _serialNumber;

        public string SerialNumber
        {
            get { return _serialNumber; }
        }


        public SIM(string serialNumber)
        {


            if (serialNumber.Length != 12)
            {
                throw new ArgumentException("SIM number is invalid");
            }
            else
                _serialNumber = serialNumber;
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                if (_phoneNumber.Length != 11)
                {
                    throw new ArgumentException("Phone number is invalid");
                }
                else
                    _phoneNumber = value;

            }
        }
    }
}
