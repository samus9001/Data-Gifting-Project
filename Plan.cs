using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGifting
{
    public class Plan
    {

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

                if (value >= 0)
                {
                    _daysLeftAllowanceRefresh = value;
                }
            }
        }

        private int _dataAmountRemaining;

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
    }
}
