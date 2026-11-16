public class ScenarioCultCommunardOffering : Scenario
{
	private string _narrative;

	private SimCar _car;

	private SimWatercraft _watercraft;

	private SimAircraft _aircraft;

	private SimJewel _jewelry;

	private SimHouse _house;

	private SimCommunard _communard;

	private double _cash;

	private string _descriptiveString;

	private bool _secondPass;

	private bool _offersMore;

	public ScenarioCultCommunardOffering(SimCommunard communard, SimCar car, bool secondPass = false, bool offersMore = false)
	{
	}

	public ScenarioCultCommunardOffering(SimCommunard communard, SimWatercraft watercraft, bool secondPass = false, bool offersMore = false)
	{
	}

	public ScenarioCultCommunardOffering(SimCommunard communard, SimAircraft aircraft, bool secondPass = false, bool offersMore = false)
	{
	}

	public ScenarioCultCommunardOffering(SimCommunard communard, SimJewel jewelry, bool secondPass = false, bool offersMore = false)
	{
	}

	public ScenarioCultCommunardOffering(SimCommunard communard, SimHouse house, bool secondPass = false, bool offersMore = false)
	{
	}

	public ScenarioCultCommunardOffering(SimCommunard communard, double cash, bool secondPass = false, bool offersMore = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
