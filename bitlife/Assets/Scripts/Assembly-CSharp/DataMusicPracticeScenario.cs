using System.Collections;

public class DataMusicPracticeScenario
{
	private string Scenario__BackingField;

	private string LogVersion__BackingField;

	private bool WorksWithAllRoles__BackingField;

	private ArrayList SpecificRoleArray__BackingField;

	public string Scenario
	{
		get
		{
			return Scenario__BackingField;
		}
		set
		{
			Scenario__BackingField = value;
		}
	}

	public string LogVersion
	{
		get
		{
			return LogVersion__BackingField;
		}
		set
		{
			LogVersion__BackingField = value;
		}
	}

	public bool WorksWithAllRoles
	{
		get
		{
			return WorksWithAllRoles__BackingField;
		}
		set
		{
			WorksWithAllRoles__BackingField = value;
		}
	}

	public ArrayList SpecificRoleArray
	{
		get
		{
			return SpecificRoleArray__BackingField;
		}
		set
		{
			SpecificRoleArray__BackingField = value;
		}
	}
}
