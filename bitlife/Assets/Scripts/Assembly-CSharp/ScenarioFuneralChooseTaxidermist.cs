using System.Collections.Generic;

public class ScenarioFuneralChooseTaxidermist : Scenario
{
	private float _cost;

	private string _personNameScenario;

	private string _personNamePossessiveScenario;

	private readonly SimParent _parent;

	private readonly SimLover _lover;

	private readonly SimChild _child;

	private readonly SimCommunard _communard;

	private readonly SimSecretAgent _secretAgent;

	private readonly SimPerson _person;

	private readonly int _personWish;

	private string _narrative;

	private List<string> _taxidermists;

	public ScenarioFuneralChooseTaxidermist(SimParent theParent, float cost, int personWish)
	{
	}

	public ScenarioFuneralChooseTaxidermist(SimLover theLover, float cost, int personWish)
	{
	}

	public ScenarioFuneralChooseTaxidermist(SimChild theChild, float cost, int personWish)
	{
	}

	public ScenarioFuneralChooseTaxidermist(SimCommunard theCommunard, float cost, int personWish)
	{
	}

	public ScenarioFuneralChooseTaxidermist(SimSecretAgent theSecretAgent, float cost, int personWish)
	{
	}

	public ScenarioFuneralChooseTaxidermist(SimParent theParent, SimLover theLover, SimChild theChild, SimCommunard theCommunard, SimSecretAgent theAgent, float cost, int personWish, List<string> taxidermists)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
