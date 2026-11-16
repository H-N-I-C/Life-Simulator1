using System.Collections;
using System.Collections.Generic;

public class ScenarioRacingRaceRepairCarMods : Scenario
{
	private string _narrative;

	private SimRaceCarMechanic _mechanicA;

	private SimRaceCarMechanic _mechanicB;

	private float _mechanicACost;

	private float _mechanicBCost;

	private readonly SimRaceCar _raceCar;

	private readonly List<SimRaceCarMod> _mods;

	public ScenarioRacingRaceRepairCarMods(SimRaceCar raceCar, List<SimRaceCarMod> mods)
	{
	}

	public override void RefreshScenario()
	{
	}

	private float GetCostToReplace(SimRaceCarMod mod)
	{
		return 0f;
	}

	private float GetCostToRepair(SimRaceCarMod mod)
	{
		return 0f;
	}

	private float CalculateTotalRepairCost()
	{
		return 0f;
	}

	private float GetCost(SimRaceCarMechanic mechanic, float estimate)
	{
		return 0f;
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private string GetRandomBadMechanicColorString(SimRaceCarMechanic mechanic)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
