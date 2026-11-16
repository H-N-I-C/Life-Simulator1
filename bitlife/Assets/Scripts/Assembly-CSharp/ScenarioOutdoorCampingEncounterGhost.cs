using Game;

public class ScenarioOutdoorCampingEncounterGhost : Scenario
{
	private readonly SimOutdoorCampingActivity _campingActivity;

	private DataOutdoorGhostEncounter _ghostEncounterDescription;

	public ScenarioOutdoorCampingEncounterGhost(SimOutdoorCampingActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void Log(string text)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private int GetTerrorLevel()
	{
		return 0;
	}

	private void FilmGhost()
	{
	}

	private bool GhostWillAttack()
	{
		return false;
	}

	private void ComplimentGhost()
	{
	}

	private void Attacked()
	{
	}

	private void PossibleDeath()
	{
	}

	private void SuddenlyAttacked()
	{
	}

	private void StayCalm()
	{
	}

	private void AskGhostToLeaveYouAlone()
	{
	}

	private string HeShe(int gender)
	{
		return null;
	}

	private string HisHer(int gender)
	{
		return null;
	}

	private string HimHer(int gender)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
