using Game;

public class ScenarioCultCommuneOutreachMissionLectureDevotee : Scenario
{
	private string _narrative;

	private SimCult _cult;

	private string _himHerString;

	private string _heSheString;

	private string _hisHerString;

	private DataStreetPerson _randomPerson;

	private double _totalOffering;

	private double _currentOffering;

	private int _numberOfLecturesRemaining;

	private int _followersGained;

	private int _audienceCount;

	private int _followersLost;

	private readonly SimPlace _thePlace;

	private string _indefinitePersonDescription;

	private readonly DataCultRecruitmentMessage _theMessage;

	public ScenarioCultCommuneOutreachMissionLectureDevotee(int numberOfLecturesRemaining, SimPlace aPlace, DataCultRecruitmentMessage message, int followersGained = 0, int audienceCount = 0, int followersLost = 0, double totalOfferingAmount = 0.0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
