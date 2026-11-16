using System;
using System.Collections;

public class ScenarioLandlordLeaseAdjustTerms : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimTenantPet, string> _003C_003E9__6_0;

		internal string _003CChoiceSelectedAtIndex_003Eb__6_0(SimTenantPet x)
		{
			return null;
		}
	}

	protected SimPropertyLeaseContract _leaseContract;

	protected SimTenant _tenant;

	protected string _currentPetPolicy;

	protected bool _needsToLower;

	public ScenarioLandlordLeaseAdjustTerms(SimPropertyLeaseContract leaseContract, bool needsToLower = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
