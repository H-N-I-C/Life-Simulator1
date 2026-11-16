using Game;

public class FightClubAdCampaignActivity : ActivityLazyCsvBase
{
	private string CompanyColorPrefix__BackingField;

	private string CompanyName__BackingField;

	private DataFightIngProduct Product__BackingField;

	private int Compensation__BackingField;

	private float Reputation__BackingField;

	private float _reception;

	public string CompanyColorPrefix
	{
		get
		{
			return CompanyColorPrefix__BackingField;
		}
		private set
		{
			CompanyColorPrefix__BackingField = value;
		}
	}

	public string CompanyName
	{
		get
		{
			return CompanyName__BackingField;
		}
		private set
		{
			CompanyName__BackingField = value;
		}
	}

	public DataFightIngProduct Product
	{
		get
		{
			return Product__BackingField;
		}
		private set
		{
			Product__BackingField = value;
		}
	}

	public int Compensation
	{
		get
		{
			return Compensation__BackingField;
		}
		private set
		{
			Compensation__BackingField = value;
		}
	}

	public float Reputation
	{
		get
		{
			return Reputation__BackingField;
		}
		private set
		{
			Reputation__BackingField = value;
		}
	}

	public FightClubAdCampaignActivity(Life life)
		: base(null)
	{
	}

	private void OnOfferChoice(ChoiceSelection selection)
	{
	}

	private void OnResultDismissed()
	{
	}

	private int GetHeroLeaguePosition(Life life)
	{
		return 0;
	}

	public static void TryChainAdCampaign(Life life, Scenario feedbackScenario)
	{
	}
}
