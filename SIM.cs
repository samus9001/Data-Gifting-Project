namespace DataGifting
{
    public class SIM
    {
        private string _serialNumber;

        public string SerialNumber
        {
            get { return _serialNumber; }
            //set
            //{
            //    if (value.Length == 12)
            //    {
            //        _serialNumber = value;
            //    }
            //}
        }

        public SIM(string serialNumberLength)
        {
            _serialNumber = serialNumberLength;

            if (_serialNumber.Length == 12) 
            {
                Console.WriteLine("SIM number is valid");
            }
                else
            {
                throw new ArgumentException("SIM number is invalid");
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
