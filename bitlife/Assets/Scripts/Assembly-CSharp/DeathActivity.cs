using System.Collections.Generic;

public class DeathActivity
{
	private DEATH_TYPE DeathType__BackingField;

	private string DescriptiveSentence__BackingField;

	private bool CanBeKilled__BackingField;

	private SimPerson Person__BackingField;

	private DataWeapon Weapon__BackingField;

	private bool _isHero;

	private Dictionary<string, string> BracketDictionary__BackingField;

	public DEATH_TYPE DeathType
	{
		get
		{
			return DeathType__BackingField;
		}
		set
		{
			DeathType__BackingField = value;
		}
	}

	public string DescriptiveSentence
	{
		get
		{
			return DescriptiveSentence__BackingField;
		}
		set
		{
			DescriptiveSentence__BackingField = value;
		}
	}

	public bool CanBeKilled
	{
		get
		{
			return CanBeKilled__BackingField;
		}
		private set
		{
			CanBeKilled__BackingField = value;
		}
	}

	public SimPerson Person
	{
		get
		{
			return Person__BackingField;
		}
		set
		{
			Person__BackingField = value;
		}
	}

	public DataWeapon Weapon
	{
		get
		{
			return Weapon__BackingField;
		}
		set
		{
			Weapon__BackingField = value;
		}
	}

	public Dictionary<string, string> BracketDictionary
	{
		get
		{
			return BracketDictionary__BackingField;
		}
		set
		{
			BracketDictionary__BackingField = value;
		}
	}

	public void AttempToKill()
	{
	}

	public Scenario GetDeathScenario()
	{
		return null;
	}

	private string GetDescriptiveSentenceByDeathType()
	{
		return null;
	}
}
