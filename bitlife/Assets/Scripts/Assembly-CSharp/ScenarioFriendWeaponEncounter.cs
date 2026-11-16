public class ScenarioFriendWeaponEncounter : Scenario
{
	protected SimPerson _friend;

	protected DataWeapon _weapon;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimPerson GetFriend()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
