using System;

[Serializable]
public class SimAstronautAgency
{
	private string AgencyTitle__BackingField;

	private string AgencyAbbreviation__BackingField;

	private string SpaceAgencyDirector__BackingField;

	public string AgencyTitle
	{
		get
		{
			return AgencyTitle__BackingField;
		}
		set
		{
			AgencyTitle__BackingField = value;
		}
	}

	public string AgencyAbbreviation
	{
		get
		{
			return AgencyAbbreviation__BackingField;
		}
		set
		{
			AgencyAbbreviation__BackingField = value;
		}
	}

	public string SpaceAgencyDirector
	{
		get
		{
			return SpaceAgencyDirector__BackingField;
		}
		set
		{
			SpaceAgencyDirector__BackingField = value;
		}
	}
}
