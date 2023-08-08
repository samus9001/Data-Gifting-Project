using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGifting
{
    public class Plan
    {
        private int _planNumber;

        public int PlanNumber
        {
            get { return _planNumber; }
            set { _planNumber = value; }
        }

        private string _planName;

        public string PlanName
        {
            get { return _planName; }
            set { _planName = value; }
        }

        private SIM _sim;

        public SIM Sim
        {
            get { return _sim; }
            set { _sim = value; }
        }

        private int _daysLeftAllowanceRefresh;

        public int DaysLeftAllowanceRefresh
        {
            get
            {
                return _daysLeftAllowanceRefresh;
            }
            set
            {
                _daysLeftAllowanceRefresh = value;
            }
        }

        private int _dataAmountRemaining;
        /// <summary>
        /// MegaBytes
        /// </summary>
        public int DataAmountRemaining
        {
            get
            {
                return _dataAmountRemaining;
            }
            set
            {
                if (value >= 0)
                {
                    _dataAmountRemaining = value;
                }
            }
        }

        private int _dataAmountPercentRemaining;
        /// <summary>
        /// MegaBytes
        /// </summary>
        public int DataAmountPercentRemaining
        {
            get
            {
                return _dataAmountPercentRemaining;
            }
            set
            {
                if (value >= 0)
                {
                    _dataAmountPercentRemaining = value;
                }
            }
        }

        private string _giftingEligibility;

        public string GiftingEligibility
        {
            get { return _giftingEligibility; }
            set { _giftingEligibility = value; }
        }
    }
}