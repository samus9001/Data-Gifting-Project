using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGifting
{
    public class DataGiftRequest
    {

        private string _giftingEligibility;

        public string GiftingEligibility
        {
            get { return _giftingEligibility; }
            set { _giftingEligibility = value; }
        }

        private int _giftDate;

        public int GiftDate
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