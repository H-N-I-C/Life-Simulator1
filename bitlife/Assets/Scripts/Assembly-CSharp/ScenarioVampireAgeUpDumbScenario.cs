using System.Collections.Generic;
using Game;

public class ScenarioVampireAgeUpDumbScenario : Scenario
{
	private delegate bool IsPersonPredicate(SimPerson person);

	public static bool DebugIsScenarioInOrder;

	public static int DebugScenarioOffset;

	private readonly SimHero _hero;

	private readonly SimVampire _heroVampire;

	private readonly SimLover _lover;

	private readonly SimPerson _friend;

	private readonly SimPerson _familiar;

	private readonly SimPerson _maker;

	private readonly SimPerson _vampire;

	private readonly DataVampireDumbScenario _scenarioData;

	private List<DataVampireDumbScenarioChoice> _choiceDatas;

	private BracketParser _parser;

	private Dictionary<string, string> _bracketDictionary;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public bool HasData()
	{
		return false;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	private static DataVampireDumbScenario GetScenarioData(ScenarioVampireAgeUpDumbScenario scenario)
	{
		return null;
	}

	private static bool IsPersonVampire(SimPerson person)
	{
		return false;
	}

	private static SimPerson GetRandomPerson(ICollection<SimPerson> persons, IsPersonPredicate predicate = null)
	{
		return null;
	}

	private static HashSet<SimPerson> GetPersonalRelationSet(Life life)
	{
		return null;
	}

	private void ImpactHeroVampire(int notorietyImpact, int essenceImpact, int relationshipImpact, string narrativeTemplate)
	{
	}

	private void ImpactRelationshipByGenderBracket(string genderBracket, int relationshipImpact, string narrativeTemplate)
	{
	}

	private SimPerson FindPerson(string genderBracket, string narrativeTemplate)
	{
		return null;
	}

	private SimPerson GetRelatedPersonInTemplate(string bodyTemplate)
	{
		return null;
	}

	private SimPerson GetPersonByGenderBracket(string genderBracket)
	{
		return null;
	}

	private BracketParser CreateParser(string genderBracket, string callToAction)
	{
		return null;
	}

	private void PrepareParserByGenderBracket(BracketParser parser, string genderBracket)
	{
	}

	private void PrepareParserForRelationships(BracketParser parser)
	{
	}

	private int GetIntByGenderBracket(BracketParser parser, string genderBracket)
	{
		return 0;
	}

	private void PrepareParserForPerson(BracketParser parser, string personBracket, SimPerson person)
	{
	}

	private string ParseBody(string bodyTemplate)
	{
		return null;
	}

	private string ParseBodyTemplate(string bodyTemplate)
	{
		return null;
	}

	private static string FormatVampireNameByGender(GENDER gender)
	{
		return null;
	}
}
