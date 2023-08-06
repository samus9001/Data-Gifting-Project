namespace DataGifting
{
    public class SIM
    {
        private string _serialNumber;


        //TODO: implement constructor with sim serial as parameter
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            //set
            //{
            //    if (value.Length == 12)
            //    {
            //        _serialNumber = value;
            //    }
            //}
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }
    }
}
