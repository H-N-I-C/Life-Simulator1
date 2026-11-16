using System;

[Serializable]
public class LocalNotification
{
	private int NotificationType__BackingField;

	private string IntentData__BackingField;

	private DateTime IntendedFireDate__BackingField;

	private string SmallImageID__BackingField;

	private string LargeImageID__BackingField;

	private string Title__BackingField;

	private string Body__BackingField;

	private string ButtonTitle__BackingField;

	private string LogSuccess__BackingField;

	private string LogFailure__BackingField;

	private int ResponseWindowMinutes__BackingField;

	private string AppVersion__BackingField;

	private int CashAmount__BackingField;

	private SimPet WildPet__BackingField;

	private string EscapedPetIDString__BackingField;

	private string JackedCarIDString__BackingField;

	private string WinningHorseTip__BackingField;

	public int NotificationType
	{
		get
		{
			return NotificationType__BackingField;
		}
		set
		{
			NotificationType__BackingField = value;
		}
	}

	public string IntentData
	{
		get
		{
			return IntentData__BackingField;
		}
		set
		{
			IntentData__BackingField = value;
		}
	}

	public DateTime IntendedFireDate
	{
		get
		{
			return IntendedFireDate__BackingField;
		}
		set
		{
			IntendedFireDate__BackingField = value;
		}
	}

	public string SmallImageID
	{
		get
		{
			return SmallImageID__BackingField;
		}
		set
		{
			SmallImageID__BackingField = value;
		}
	}

	public string LargeImageID
	{
		get
		{
			return LargeImageID__BackingField;
		}
		set
		{
			LargeImageID__BackingField = value;
		}
	}

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		set
		{
			Title__BackingField = value;
		}
	}

	public string Body
	{
		get
		{
			return Body__BackingField;
		}
		set
		{
			Body__BackingField = value;
		}
	}

	public string ButtonTitle
	{
		get
		{
			return ButtonTitle__BackingField;
		}
		set
		{
			ButtonTitle__BackingField = value;
		}
	}

	public string LogSuccess
	{
		get
		{
			return LogSuccess__BackingField;
		}
		set
		{
			LogSuccess__BackingField = value;
		}
	}

	public string LogFailure
	{
		get
		{
			return LogFailure__BackingField;
		}
		set
		{
			LogFailure__BackingField = value;
		}
	}

	public int ResponseWindowMinutes
	{
		get
		{
			return ResponseWindowMinutes__BackingField;
		}
		set
		{
			ResponseWindowMinutes__BackingField = value;
		}
	}

	public string AppVersion
	{
		get
		{
			return AppVersion__BackingField;
		}
		set
		{
			AppVersion__BackingField = value;
		}
	}

	public int CashAmount
	{
		get
		{
			return CashAmount__BackingField;
		}
		set
		{
			CashAmount__BackingField = value;
		}
	}

	public SimPet WildPet
	{
		get
		{
			return WildPet__BackingField;
		}
		set
		{
			WildPet__BackingField = value;
		}
	}

	public string EscapedPetIDString
	{
		get
		{
			return EscapedPetIDString__BackingField;
		}
		set
		{
			EscapedPetIDString__BackingField = value;
		}
	}

	public string JackedCarIDString
	{
		get
		{
			return JackedCarIDString__BackingField;
		}
		set
		{
			JackedCarIDString__BackingField = value;
		}
	}

	public string WinningHorseTip
	{
		get
		{
			return WinningHorseTip__BackingField;
		}
		set
		{
			WinningHorseTip__BackingField = value;
		}
	}

	public LocalNotification(int theNotificationType, int id)
	{
	}

	public static bool IsHeroEligibleForNotificationType(int theType)
	{
		return false;
	}

	public static bool IsHeroEligibleForNotificationBait(DataNotificationBait bait)
	{
		return false;
	}

	public static bool IsHeroEligibleForNotificationVoice(DataNotificationVoice notification)
	{
		return false;
	}

	public static string StringForNotificationType(int theType)
	{
		return null;
	}

	public void SetSmallAndLargeImageID()
	{
	}

	public string GetBundledImageFileNameForNotification()
	{
		return null;
	}

	public void HandleNotificationResponseInTime(bool inTime)
	{
	}

	public void SetupNotification()
	{
	}
}
