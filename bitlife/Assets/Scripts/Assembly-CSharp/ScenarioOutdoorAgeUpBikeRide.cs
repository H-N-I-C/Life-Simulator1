using System;

public class ScenarioOutdoorAgeUpBikeRide : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRecreationalVehicle, bool> _003C_003E9__4_0;

		public static Func<SimPerson, bool> _003C_003E9__4_1;

		public static Func<SimRecreationalVehicle, bool> _003C_003E9__7_0;

		public static Func<SimPerson, bool> _003C_003E9__7_1;

		internal bool _003CRefreshScenario_003Eb__4_0(SimRecreationalVehicle t)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__4_1(SimPerson t)
		{
			return false;
		}

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__7_0(SimRecreationalVehicle t)
		{
			return false;
		}

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__7_1(SimPerson t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimPerson _friend;

	private SimRecreationalVehicle _bike;

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

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
