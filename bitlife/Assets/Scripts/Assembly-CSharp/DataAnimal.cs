using System.Collections.Generic;

public class DataAnimal
{
	private string Animal__BackingField;

	private int DangerScore__BackingField;

	private int AggressionScore__BackingField;

	private int SkittishnessScore__BackingField;

	private string AttackString__BackingField;

	private int RabidScore__BackingField;

	public List<string> ContinentArray;

	private int VampireEssenceGain__BackingField;

	public string Animal
	{
		get
		{
			return Animal__BackingField;
		}
		set
		{
			Animal__BackingField = value;
		}
	}

	public int DangerScore
	{
		get
		{
			return DangerScore__BackingField;
		}
		set
		{
			DangerScore__BackingField = value;
		}
	}

	public int AggressionScore
	{
		get
		{
			return AggressionScore__BackingField;
		}
		set
		{
			AggressionScore__BackingField = value;
		}
	}

	public int SkittishnessScore
	{
		get
		{
			return SkittishnessScore__BackingField;
		}
		set
		{
			SkittishnessScore__BackingField = value;
		}
	}

	public string AttackString
	{
		get
		{
			return AttackString__BackingField;
		}
		set
		{
			AttackString__BackingField = value;
		}
	}

	public int RabidScore
	{
		get
		{
			return RabidScore__BackingField;
		}
		set
		{
			RabidScore__BackingField = value;
		}
	}

	public int VampireEssenceGain
	{
		get
		{
			return VampireEssenceGain__BackingField;
		}
		set
		{
			VampireEssenceGain__BackingField = value;
		}
	}
}
