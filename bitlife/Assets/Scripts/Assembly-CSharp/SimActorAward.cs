using System;

[Serializable]
public class SimActorAward
{
	private int Type__BackingField;

	private int Category__BackingField;

	private int Outcome__BackingField;

	private int Genre__BackingField;

	private bool HeroRefusedToAttend__BackingField;

	private bool HeroReactedPoorly__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public int Category
	{
		get
		{
			return Category__BackingField;
		}
		set
		{
			Category__BackingField = value;
		}
	}

	public int Outcome
	{
		get
		{
			return Outcome__BackingField;
		}
		set
		{
			Outcome__BackingField = value;
		}
	}

	public int Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public bool HeroRefusedToAttend
	{
		get
		{
			return HeroRefusedToAttend__BackingField;
		}
		set
		{
			HeroRefusedToAttend__BackingField = value;
		}
	}

	public bool HeroReactedPoorly
	{
		get
		{
			return HeroReactedPoorly__BackingField;
		}
		set
		{
			HeroReactedPoorly__BackingField = value;
		}
	}

	public string GetStringForAward()
	{
		return null;
	}

	public string GetStringForAwardType(bool includeAward)
	{
		return null;
	}
}
