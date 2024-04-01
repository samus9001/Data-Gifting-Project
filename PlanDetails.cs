using DataGifting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGifting
{
    public class PlanDetails
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

        private bool _userEligibleForGifting;

        public bool UserEligibleForGifting
        {
            get { return _userEligibleForGifting; }
            set { _userEligibleForGifting = value; }
        }

        private int _totalNumberOfSubscriptions;

        public int TotalNumberOfSubscriptions
        {
            get { return _totalNumberOfSubscriptions; }
            set { _totalNumberOfSubscriptions = value; }
        }

        private SIM _sim;

        public SIM Sim
        {
            get { return _sim; }
            set { _sim = value; }
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

        private string _allowanceUsed;
        public string AllowanceUsed
        {
            get { return _allowanceUsed; }
            set { _allowanceUsed = value; }
        }

        public string allowanceUsed { get; set; }


        private string _allowanceUsedUnits;

        public string AllowanceUsedUnits
        {
            get { return _allowanceUsedUnits; }
            set { _allowanceUsedUnits = value; }
        }

        private string _allowanceAvailable;

        public string AllowanceAvailable
        {
            get { return _allowanceAvailable; }
            set { _allowanceAvailable = value; }
        }

        private string _allowanceAvailableUnits;

        public string AllowanceAvailableUnits
        {
            get { return _allowanceAvailableUnits; }
            set { _allowanceAvailableUnits = value; }
        }

        private string _allowanceLeft;

        public string AllowanceLeft
        {
            get { return _allowanceLeft; }
            set { _allowanceLeft = value; }
        }

        private string _allowanceLeftUnits;

        public string AllowanceLeftUnits
        {
            get { return _allowanceLeftUnits; }
            set { _allowanceLeftUnits = value; }
        }

        private string _daysLeftInBillPeriod;

        public string DaysLeftInBillPeriod
        {
            get { return _daysLeftInBillPeriod; }
            set { _daysLeftInBillPeriod = value; }
        }

        private string _allowanceRefreshDate;

        public string AllowanceRefreshDate
        {
            get { return _allowanceRefreshDate; }
            set { _allowanceRefreshDate = value; }
        }

        private string _allowanceRefreshDay;
        public string AllowanceRefreshDay
        {
            get { return _allowanceRefreshDay; }
            set { _allowanceRefreshDay = value; }
        }

        private string _allowanceRefreshDayMonth;
        public string AllowanceRefreshDayMonth
        {
            get { return _allowanceRefreshDayMonth; }
            set { _allowanceRefreshDayMonth = value; }
        }
    }
}