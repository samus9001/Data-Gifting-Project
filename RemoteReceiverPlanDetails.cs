using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGifting
{
	internal class RemoteReceiverPlanDetails
	{
		public class DeviceDetails
		{
			public string manufacturer { get; set; }
			public string deviceName { get; set; }
			public object thumbnailUrl { get; set; }
			public object imageUrl { get; set; }
		}

		public class UsageDatapassProgressBarComponent
		{
			public string allowanceUsed { get; set; }
			public string allowanceUsedUnits { get; set; }
			public string allowanceAvailable { get; set; }
			public string allowanceAvailableUnits { get; set; }
			public string allowanceLeft { get; set; }
			public string allowanceLeftUnits { get; set; }
			public bool shared { get; set; }
			public string daysLeftInBillPeriod { get; set; }
			public bool multipleDataPassPresentBillPeriod { get; set; }
			public object flowName { get; set; }
			public object planName { get; set; }
			public bool low { get; set; }
			public bool @out { get; set; }
			public bool allowancesDataError { get; set; }
			public bool unlimited { get; set; }
			public bool throttled { get; set; }
			public bool stayConnected { get; set; }
		}

		public class YourDataAllowancesSection
		{
			public UsageDatapassProgressBarComponent usageDatapassProgressBarComponent { get; set; }
			public bool error { get; set; }
			public bool stayConnected { get; set; }
			public bool availableDataPassesForLowOrZeroAllowances { get; set; }
		}

		public YourDataAllowancesSection yourDataAllowancesSection { get; set; }
		public string allowanceRefreshDate { get; set; }
		public string allowanceRefreshDay { get; set; }
		public string allowanceRefreshDayMonth { get; set; }
		public DeviceDetails deviceDetails { get; set; }
		public bool showNotHappyWithYourPlanNotification { get; set; }
		public bool showPlanChangeInProgress { get; set; }

		// Extract desired properties and return to PlanDetails object
		public ReceiverPlanDetails ExtractReceiverPlanDetails()
		{
			ReceiverPlanDetails receiverPlanDetails = new ReceiverPlanDetails();

			receiverPlanDetails.AllowanceUsed = yourDataAllowancesSection.usageDatapassProgressBarComponent.allowanceUsed;
			receiverPlanDetails.AllowanceUsedUnits = yourDataAllowancesSection.usageDatapassProgressBarComponent.allowanceUsedUnits;
			receiverPlanDetails.AllowanceAvailable = yourDataAllowancesSection.usageDatapassProgressBarComponent.allowanceAvailable;
			receiverPlanDetails.AllowanceAvailableUnits = yourDataAllowancesSection.usageDatapassProgressBarComponent.allowanceAvailableUnits;
			receiverPlanDetails.AllowanceLeft = yourDataAllowancesSection.usageDatapassProgressBarComponent.allowanceLeft;
			receiverPlanDetails.AllowanceLeftUnits = yourDataAllowancesSection.usageDatapassProgressBarComponent.allowanceLeftUnits;
			receiverPlanDetails.DaysLeftInBillPeriod = yourDataAllowancesSection.usageDatapassProgressBarComponent.daysLeftInBillPeriod;
			receiverPlanDetails.AllowanceRefreshDate = allowanceRefreshDate;
			receiverPlanDetails.AllowanceRefreshDay = allowanceRefreshDay;
			receiverPlanDetails.AllowanceRefreshDayMonth = allowanceRefreshDayMonth;

			return receiverPlanDetails;
		}

		// Extract DeviceDetails properties and set them in PhoneDetails
		public ReceiverPhoneDetails ExtractReceiverPhoneDetails()
		{
			ReceiverPhoneDetails receiverPhoneDetails = new ReceiverPhoneDetails();

			receiverPhoneDetails.Manufacturer = deviceDetails.manufacturer;
			receiverPhoneDetails.DeviceName = deviceDetails.deviceName;

			return receiverPhoneDetails;
		}
	}
}
