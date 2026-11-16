using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimAthleteSeasonStats
{
	private int TeamIndex__BackingField;

	private SimSportsTeamRecord TeamRecord__BackingField;

	private int InjuryMissedGames__BackingField;

	private string PositionString__BackingField;

	private string AwardString__BackingField;

	private Dictionary<string, string> StatDictionary__BackingField;

	private ArrayList _StatOrderArray__BackingField;

	private ArrayList _SkillArray__BackingField;

	private float GreatnessAttribute__BackingField;

	private float RespectAttribute__BackingField;

	private float FitnessAttribute__BackingField;

	public int TeamIndex
	{
		get
		{
			return TeamIndex__BackingField;
		}
		set
		{
			TeamIndex__BackingField = value;
		}
	}

	public SimSportsTeamRecord TeamRecord
	{
		get
		{
			return TeamRecord__BackingField;
		}
		set
		{
			TeamRecord__BackingField = value;
		}
	}

	public int InjuryMissedGames
	{
		get
		{
			return InjuryMissedGames__BackingField;
		}
		set
		{
			InjuryMissedGames__BackingField = value;
		}
	}

	public string PositionString
	{
		get
		{
			return PositionString__BackingField;
		}
		set
		{
			PositionString__BackingField = value;
		}
	}

	public string AwardString
	{
		get
		{
			return AwardString__BackingField;
		}
		set
		{
			AwardString__BackingField = value;
		}
	}

	public Dictionary<string, string> StatDictionary
	{
		get
		{
			return StatDictionary__BackingField;
		}
		set
		{
			StatDictionary__BackingField = value;
		}
	}

	private ArrayList _StatOrderArray
	{
		get
		{
			return _StatOrderArray__BackingField;
		}
		set
		{
			_StatOrderArray__BackingField = value;
		}
	}

	public ArrayList StatOrderArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _SkillArray
	{
		get
		{
			return _SkillArray__BackingField;
		}
		set
		{
			_SkillArray__BackingField = value;
		}
	}

	public ArrayList SkillArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float GreatnessAttribute
	{
		get
		{
			return GreatnessAttribute__BackingField;
		}
		set
		{
			GreatnessAttribute__BackingField = value;
		}
	}

	public float RespectAttribute
	{
		get
		{
			return RespectAttribute__BackingField;
		}
		set
		{
			RespectAttribute__BackingField = value;
		}
	}

	public float FitnessAttribute
	{
		get
		{
			return FitnessAttribute__BackingField;
		}
		set
		{
			FitnessAttribute__BackingField = value;
		}
	}
}
