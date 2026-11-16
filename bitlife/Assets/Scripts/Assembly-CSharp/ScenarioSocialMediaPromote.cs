public class ScenarioSocialMediaPromote : Scenario
{
	protected IPromotableSocialMedia _socialMediaChannel;

	protected DataSocialMediaPromotionProduct _product;

	private string ProductDescriptiveString__BackingField;

	private float Compensation__BackingField;

	private int ResponseScore__BackingField;

	private int SubscribersLost__BackingField;

	private string _Narrative;

	protected string ProductDescriptiveString
	{
		get
		{
			return ProductDescriptiveString__BackingField;
		}
		private set
		{
			ProductDescriptiveString__BackingField = value;
		}
	}

	protected float Compensation
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

	protected int ResponseScore
	{
		get
		{
			return ResponseScore__BackingField;
		}
		private set
		{
			ResponseScore__BackingField = value;
		}
	}

	protected int SubscribersLost
	{
		get
		{
			return SubscribersLost__BackingField;
		}
		private set
		{
			SubscribersLost__BackingField = value;
		}
	}

	public ScenarioSocialMediaPromote()
	{
	}

	public ScenarioSocialMediaPromote(IPromotableSocialMedia socialMediaChannel)
	{
	}

	public ScenarioSocialMediaPromote(int theChannel)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimSocialMediaChannel GetSocialMediaChannel()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override void Closed()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
