using System.Collections;
using System.Collections.Generic;
using Game;

public class FightClubRechargeActivity : ActivityLazyCsvBase
{
	private const float MAX_STAMINA = 100f;

	private const int FREE_RECHARGE_STAMINA_GAIN = 5;

	private float OldStamina__BackingField;

	private DataFightRechargeMethodScenario MethodScenarioData__BackingField;

	private readonly SimFightClub _club;

	private readonly SimFightClubFighterStats _stats;

	private readonly SimHero _hero;

	private readonly ChoiceRewardedVideo _rewardedVideo;

	public float OldStamina
	{
		get
		{
			return OldStamina__BackingField;
		}
		private set
		{
			OldStamina__BackingField = value;
		}
	}

	public DataFightRechargeMethodScenario MethodScenarioData
	{
		get
		{
			return MethodScenarioData__BackingField;
		}
		private set
		{
			MethodScenarioData__BackingField = value;
		}
	}

	public SimFightClub Club => null;

	public FightClubRechargeActivity(Life life)
		: base(null)
	{
	}

	public bool IsEligible()
	{
		return false;
	}

	public float GetStamina()
	{
		return 0f;
	}

	public double GetStreetCred()
	{
		return 0.0;
	}

	public List<DataFightRechargeMethod> GetMethods()
	{
		return null;
	}

	public void AddRewardedVideoChoice(ArrayList scenarioChoices)
	{
	}

	public void Recharge(ChoiceSelection selection)
	{
	}

	public void OnVideoCompleteImpactByVIPTreatment()
	{
	}

	private void RechargeByDropDownIndex(int dropDownIndex)
	{
	}

	private void RechargeByVipTreatment()
	{
	}
}
