using Game;

public class ScenarioCultCommuneOutreachMissionLectureHeckler : Scenario
{
	private string _narrative;

	private SimCult _cult;

	private string _genderString;

	private DataStreetPerson _randomPerson;

	private int _numberOfLecturesRemaining;

	private int _followersGained;

	private int _audienceCount;

	private double _totalOffering;

	private readonly SimPlace _thePlace;

	private DataCultRecruitmentMessage _theMessage;

	private int _followersLost;

	public ScenarioCultCommuneOutreachMissionLectureHeckler(int numberOfLecturesRemaining, SimPlace aPlace, DataCultRecruitmentMessage message, int followersGained = 0, int audienceCount = 0, int followersLost = 0, double totalOffering = 0.0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void MoveOnToNextLecture()
	{
	}
}
