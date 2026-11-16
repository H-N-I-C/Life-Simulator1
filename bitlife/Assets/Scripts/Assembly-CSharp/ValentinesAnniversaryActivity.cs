using Game;

public class ValentinesAnniversaryActivity
{
	public enum METHOD
	{
		THREESOME = 0,
		GIFT = 1,
		DATE = 2
	}

	public enum OUTCOME
	{
		MAKE_LOVE = 0,
		TOGETHER_FOREVER = 1,
		GIFT = 2
	}

	private METHOD Method__BackingField;

	private SimLover Lover__BackingField;

	private OUTCOME Outcome__BackingField;

	private DataAnniversaryDate SpecialDateData__BackingField;

	private SimJewel Jewel__BackingField;

	private int JewelCost__BackingField;

	private bool WantToPropose__BackingField;

	private SimLover ThreesomePartner__BackingField;

	private int ThreesomeAttempts__BackingField;

	private DataAnniversaryGift Gift__BackingField;

	private DataAnniversaryGift LoverGiftOffer__BackingField;

	private SimHero _hero;

	private Life _life;

	private float _minLoverMoney;

	private float _maxLoverMoney;

	public METHOD Method
	{
		get
		{
			return Method__BackingField;
		}
		set
		{
			Method__BackingField = value;
		}
	}

	public SimLover Lover
	{
		get
		{
			return Lover__BackingField;
		}
		private set
		{
			Lover__BackingField = value;
		}
	}

	public OUTCOME Outcome
	{
		get
		{
			return Outcome__BackingField;
		}
		private set
		{
			Outcome__BackingField = value;
		}
	}

	public DataAnniversaryDate SpecialDateData
	{
		get
		{
			return SpecialDateData__BackingField;
		}
		private set
		{
			SpecialDateData__BackingField = value;
		}
	}

	public SimJewel Jewel
	{
		get
		{
			return Jewel__BackingField;
		}
		set
		{
			Jewel__BackingField = value;
		}
	}

	public int JewelCost
	{
		get
		{
			return JewelCost__BackingField;
		}
		set
		{
			JewelCost__BackingField = value;
		}
	}

	public bool WantToPropose
	{
		get
		{
			return WantToPropose__BackingField;
		}
		private set
		{
			WantToPropose__BackingField = value;
		}
	}

	public SimLover ThreesomePartner
	{
		get
		{
			return ThreesomePartner__BackingField;
		}
		private set
		{
			ThreesomePartner__BackingField = value;
		}
	}

	public int ThreesomeAttempts
	{
		get
		{
			return ThreesomeAttempts__BackingField;
		}
		private set
		{
			ThreesomeAttempts__BackingField = value;
		}
	}

	public DataAnniversaryGift Gift
	{
		get
		{
			return Gift__BackingField;
		}
		private set
		{
			Gift__BackingField = value;
		}
	}

	public DataAnniversaryGift LoverGiftOffer
	{
		get
		{
			return LoverGiftOffer__BackingField;
		}
		private set
		{
			LoverGiftOffer__BackingField = value;
		}
	}

	public ValentinesAnniversaryActivity(SimLover lover)
	{
	}

	public void StartCelebration()
	{
	}

	public void SelectSpecialDate(DataAnniversaryDate selectedDate)
	{
	}

	private void CreateJewel()
	{
	}

	private void CalculateLoversMoney()
	{
	}

	private void CalculateOutcome()
	{
	}

	public void AcceptGift()
	{
	}

	public void RefuseGift()
	{
	}

	public void ApplyLoveMakingRefusalImpact()
	{
	}

	public void GenerateThreesomePartner()
	{
	}

	public void GoThroughThreesome(Scenario current)
	{
	}

	private void CheckPregnancy(SimPerson mother, SimPerson father, Scenario currentScenario)
	{
	}

	public void SelectGift(DataAnniversaryGift selectedGift)
	{
	}

	public void WatchSpicyVideo()
	{
	}

	public void ApplyLoveMakingAcceptanceImpact(float heroEnjoyment, float loverEnjoyment)
	{
	}
}
