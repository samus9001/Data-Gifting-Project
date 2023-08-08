using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGifting
{
    public class DataGiftRequest
    {
        private int _sourcePhoneNumber;

        public int SourcePhoneNumber
        {
            get { return _sourcePhoneNumber; }
            set { _sourcePhoneNumber = value; }
        }

        private int _destinationPhoneNumber;

        public int DestinationPhoneNumber
        {
            get { return _destinationPhoneNumber; }
            set { _destinationPhoneNumber = value; }
        }

        private DateTime _giftDate;

        public DateTime GiftDate
        {
            get { return _giftDate; }
            set { _giftDate = value; }
        }

        private int _giftAmount;

        public int GiftAmount
        {
            get
            {
                return _giftAmount;
            }
            set
            {
                if (value >= 0)
                {
                    _giftAmount = value;
                }
            }
        }
    }
}