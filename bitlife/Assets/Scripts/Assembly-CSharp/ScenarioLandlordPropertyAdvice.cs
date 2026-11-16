using System;

public class ScenarioLandlordPropertyAdvice : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPerson, bool> _003C_003E9__2_0;

		public static Func<SimPerson, bool> _003C_003E9__5_0;

		internal bool _003C_002Ector_003Eb__2_0(SimPerson t)
		{
			return false;
		}

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__5_0(SimPerson t)
		{
			return false;
		}
	}

	private SimPerson _friend;

	private SimHouse _house;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	private static bool IsEligibleForAdvice()
	{
		return false;
	}
}
