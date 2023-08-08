namespace DataGifting
{
    public class SIM
    {
        private string _serialNumber;

        public string SerialNumber
        {
            get { return _serialNumber; }
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
        }

        //CS0111 error (duplicate type?)
        //public SIM(string phoneNumberLength)
        //{
        //    _phoneNumber = phoneNumberLength;

        //    if (phoneNumberLength.Length == 11)
        //    {
        //        Console.WriteLine("Phone number is valid");
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Phone number is invalid");
        //    }
        //}
    }
}
