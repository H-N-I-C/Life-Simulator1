using System.Collections;

public class ScenarioRacingRaceCarModRepairOrReplacePickMechanic : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private readonly SimRaceCar _raceCar;

	private readonly SimRaceCarMod _mod;

	private readonly bool _replace;

	private SimRaceCarMechanic _mechanicA;

	private SimRaceCarMechanic _mechanicB;

	private float _costMechanicA;

	private float _costMechanicB;

	private readonly float _estimatedCost;

	private readonly BracketParser _parser;

	public ScenarioRacingRaceCarModRepairOrReplacePickMechanic(SimRaceCar raceCar, SimRaceCarMod mod, bool replace, float estimatedCost)
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
