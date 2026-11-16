using Game;

public class ScenarioOutdoorCampingEncounterPlant : Scenario
{
	private string _narrative;

	private DataOutdoorPlant _plantEncountered;

	private bool _plantWillKill;

	private bool _plantWasDiscovered;

	private readonly SimOutdoorCampingActivity _outdoorCamping;

	public ScenarioOutdoorCampingEncounterPlant(SimOutdoorCampingActivity campingActivity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void PetEatsIt()
	{
	}

	private void PetRefusesToEat()
	{
	}

	private void PetNibblesPlant()
	{
	}

	private void PetEatsPlant()
	{
	}

	private void TakeANibble()
	{
	}

	private int GetToxicDeathChance()
	{
		return 0;
	}

	private void EatTheWholeThing()
	{
	}

	private void LeaveItAlone()
	{
	}

	private void GuestEatsPlant()
	{
	}

	private void GuestNibblesPlant()
	{
	}

	private void GuestRefusesToEatPlant()
	{
	}

	private bool GuestWillpowerCheck()
	{
		return false;
	}

	private bool GuestIntelligenceCheck()
	{
		return false;
	}

	private string RandomColor()
	{
		return null;
	}
}
