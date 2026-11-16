using System.Collections;

public class DataMusicAuditionRejection
{
	private string Reason__BackingField;

	private bool Solo__BackingField;

	private bool Band__BackingField;

	private bool WorksWithAllRoles__BackingField;

	private ArrayList SpecificRoleArray__BackingField;

	public string Reason
	{
		get
		{
			return Reason__BackingField;
		}
		set
		{
			Reason__BackingField = value;
		}
	}

	public bool Solo
	{
		get
		{
			return Solo__BackingField;
		}
		set
		{
			Solo__BackingField = value;
		}
	}

	public bool Band
	{
		get
		{
			return Band__BackingField;
		}
		set
		{
			Band__BackingField = value;
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
