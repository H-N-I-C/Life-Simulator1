using System.Collections;

public class ScenarioRacingRaceCarModInstallPickMechanic : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private SimRaceCarMod _mod;

	private SimRaceCar _raceCar;

	private SimRaceCarMechanic _mechanicA;

	private SimRaceCarMechanic _mechanicB;

	private float _costMechanicA;

	private float _costMechanicB;

	private float _estimatedCost;

	public ScenarioRacingRaceCarModInstallPickMechanic(SimRaceCar raceCar, SimRaceCarMod mod, float estimatedCost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private float GetCost(SimRaceCarMechanic mechanic)
	{
		return 0f;
	}

	public override string EmojiString()
	{
		return null;
	}
}
