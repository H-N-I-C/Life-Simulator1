using System;

public class ScenarioLandlordTenantLoverNag : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPropertyLeaseContract, bool> _003C_003E9__3_0;

		public static Func<SimPropertyLeaseContract, bool> _003C_003E9__5_0;

		internal bool _003CRefreshScenario_003Eb__3_0(SimPropertyLeaseContract t)
		{
			return false;
		}

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__5_0(SimPropertyLeaseContract t)
		{
			return false;
		}
	}

	protected SimPropertyLeaseContract _contract;

	protected SimTenant _tenant;

	protected string _narrative;

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
}
