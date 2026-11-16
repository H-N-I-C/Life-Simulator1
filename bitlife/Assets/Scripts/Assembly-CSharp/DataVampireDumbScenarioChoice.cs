using System.Collections.Generic;
using Game;

public class DataVampireDumbScenarioChoice
{
	private string Choice__BackingField;

	private string Narrative__BackingField;

	private int NotorietyImpact__BackingField;

	private int EssenceImpact__BackingField;

	private int RelationshipImpact__BackingField;

	public string Choice
	{
		get
		{
			return Choice__BackingField;
		}
		set
		{
			Choice__BackingField = value;
		}
	}

	public string Narrative
	{
		get
		{
			return Narrative__BackingField;
		}
		set
		{
			Narrative__BackingField = value;
		}
	}

	public int NotorietyImpact
	{
		get
		{
			return NotorietyImpact__BackingField;
		}
		set
		{
			NotorietyImpact__BackingField = value;
		}
	}

	public int EssenceImpact
	{
		get
		{
			return EssenceImpact__BackingField;
		}
		set
		{
			EssenceImpact__BackingField = value;
		}
	}

	public int RelationshipImpact
	{
		get
		{
			return RelationshipImpact__BackingField;
		}
		set
		{
			RelationshipImpact__BackingField = value;
		}
	}

	public static List<DataVampireDumbScenarioChoice> CreateChoices(DataVampireDumbScenario scenarioData)
	{
		return null;
	}

	private static void AddValidChoice(string choice, string narrative, int notorietyImpact, int essenceImpact, int relationshipImpact, List<DataVampireDumbScenarioChoice> choiceDatas)
	{
	}
}
