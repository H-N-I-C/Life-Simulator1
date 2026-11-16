using System;
using System.Collections.Generic;

[Serializable]
public class SimZooAttractionCommentCard
{
	private string Color__BackingField;

	private string PersonName__BackingField;

	private GENDER PersonGender__BackingField;

	private SimZooAttraction Attraction__BackingField;

	private bool HasBeenViewed__BackingField;

	public string Color
	{
		get
		{
			return Color__BackingField;
		}
		set
		{
			Color__BackingField = value;
		}
	}

	public string PersonName
	{
		get
		{
			return PersonName__BackingField;
		}
		set
		{
			PersonName__BackingField = value;
		}
	}

	public GENDER PersonGender
	{
		get
		{
			return PersonGender__BackingField;
		}
		set
		{
			PersonGender__BackingField = value;
		}
	}

	public SimZooAttraction Attraction
	{
		get
		{
			return Attraction__BackingField;
		}
		set
		{
			Attraction__BackingField = value;
		}
	}

	public bool HasBeenViewed
	{
		get
		{
			return HasBeenViewed__BackingField;
		}
		set
		{
			HasBeenViewed__BackingField = value;
		}
	}

	public SimZooAttractionCommentCard(List<SimZooAttraction> exclusionList)
	{
	}
}
