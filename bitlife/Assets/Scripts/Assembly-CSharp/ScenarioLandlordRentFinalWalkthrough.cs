using System;

public class ScenarioLandlordRentFinalWalkthrough : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimTenantPet, int> _003C_003E9__9_0;

		internal int _003CRefreshScenario_003Eb__9_0(SimTenantPet x)
		{
			return 0;
		}
	}

	protected SimTenant _tenant;

	protected SimPropertyLeaseContract _contract;

	public double _cost;

	protected double _damage;

	protected string _narrative;

	protected bool _tenantMovedOut;

	protected string _walkthroughString;

	protected bool isFirstPass;

	public ScenarioLandlordRentFinalWalkthrough(SimPropertyLeaseContract contract, bool tenantMoveOut = false, double damage = 0.0, string walkthroughString = null, string narrative = null, double cost = 0.0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
