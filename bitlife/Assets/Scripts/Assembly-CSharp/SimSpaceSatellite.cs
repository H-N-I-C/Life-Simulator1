using System;
using System.Collections.Generic;

[Serializable]
public class SimSpaceSatellite
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string, string> _003C_003E9__20_0;

		internal string _003CGetObjectString_003Eb__20_0(string i, string j)
		{
			return null;
		}
	}

	private string Name__BackingField;

	private List<string> RepairParts__BackingField;

	private bool IsRepaired__BackingField;

	private float Att_health__BackingField;

	private int RiskScore__BackingField;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public List<string> RepairParts
	{
		get
		{
			return RepairParts__BackingField;
		}
		set
		{
			RepairParts__BackingField = value;
		}
	}

	public bool IsRepaired
	{
		get
		{
			return IsRepaired__BackingField;
		}
		set
		{
			IsRepaired__BackingField = value;
		}
	}

	public float Att_health
	{
		get
		{
			return Att_health__BackingField;
		}
		set
		{
			Att_health__BackingField = value;
		}
	}

	public int RiskScore
	{
		get
		{
			return RiskScore__BackingField;
		}
		set
		{
			RiskScore__BackingField = value;
		}
	}

	public string GetObjectString()
	{
		return null;
	}

	public void Repair()
	{
	}

	public void ImpactHealth(int impactScore)
	{
	}

	public string GetRepairPriorityItem()
	{
		return null;
	}

	public List<string> GetRepairSecondaryItems()
	{
		return null;
	}

	public DataAstronautSatellitePart GetRepairPriorityItemFromItem(string part)
	{
		return null;
	}
}
