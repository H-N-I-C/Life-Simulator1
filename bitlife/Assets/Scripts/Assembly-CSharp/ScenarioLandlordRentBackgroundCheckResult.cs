using System.Collections.Generic;

public class ScenarioLandlordRentBackgroundCheckResult : Scenario
{
	protected SimTenant _applicant;

	protected SimHouse _property;

	protected double _rent;

	protected double _deposit;

	protected int _petPolicy;

	protected List<SimTenant> _applicantsList;

	protected string _keyFeatureString;

	public ScenarioLandlordRentBackgroundCheckResult(List<SimTenant> applicantsList, SimHouse property, double rent, double deposit, int petPolicy, string keyFeatureString)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
