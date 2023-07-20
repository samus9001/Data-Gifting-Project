using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGifting
{
    public class Address
    {
        private string _houseNumber;

        public string HouseNumber
        {
            get { return _houseNumber; }
            set { _houseNumber = value; }
        }

        private string _streetName;

        public string StreetName
        {
            get { return _streetName; }
            set { _streetName = value; }
        }

        private string _postCode;

        public string PostCode
        {
            get { return _postCode; }
            set { _postCode = value; }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
    }
}
