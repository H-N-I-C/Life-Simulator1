using System;

public class ScenarioLandlordRentApplicantApproved : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<SimPropertyRemodel> _003C_003E9__7_0;

		internal bool _003CRefreshScenario_003Eb__7_0(SimPropertyRemodel x)
		{
			return false;
		}
	}

	protected SimTenant _applicant;

	protected SimHouse _property;

	protected double _rent;

	protected double _deposit;

	protected int _petPolicy;

	protected string _keyFeatureString;

	public ScenarioLandlordRentApplicantApproved(SimTenant applicant, SimHouse property, double rent, double deposit, int petPolicy, string keyFeatureString)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
