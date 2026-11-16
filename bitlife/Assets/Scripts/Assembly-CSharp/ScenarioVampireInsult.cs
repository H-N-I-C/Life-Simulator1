public class ScenarioVampireInsult : Scenario
{
	private string _narrative;

	private readonly SimPerson _person;

	private bool _heroIsVampire;

	private bool _personIsVampire;

	private string RelationshipString__BackingField;

	public string RelationshipString
	{
		get
		{
			return RelationshipString__BackingField;
		}
		set
		{
			RelationshipString__BackingField = value;
		}
	}

	public ScenarioVampireInsult(SimPerson person)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
