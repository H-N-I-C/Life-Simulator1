using System;

public class ScenarioOutdoorAgeUpParentsGiveBike : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRecreationalVehicle, bool> _003C_003E9__8_0;

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__8_0(SimRecreationalVehicle t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimRecreationalVehicle _bike;

	private string _parentsString;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public static bool WillGetBicycle()
	{
		return false;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
