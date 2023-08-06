using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGifting
{
    public class DataGiftRequest
    {

        //TODO: source and destination plan properties

 

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