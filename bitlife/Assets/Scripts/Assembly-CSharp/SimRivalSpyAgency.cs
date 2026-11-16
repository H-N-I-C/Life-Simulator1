using System;
using System.Collections.Generic;

[Serializable]
public class SimRivalSpyAgency
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSecretAgent, bool> _003C_003E9__39_0;

		internal bool _003CGetInfiltrationAgents_003Eb__39_0(SimSecretAgent t)
		{
			return false;
		}
	}

	private string LongName__BackingField;

	private string ShortName__BackingField;

	private SECRET_AGENT_RIVAL_AGENCY_SECURITY_LEVEL SecurityLevel__BackingField;

	private double BankBalance__BackingField;

	private List<SimSecretAgentMission> MissionsList__BackingField;

	private List<SimSecretAgent> AgentsList__BackingField;

	private int AgeAtLastSecretAgentRefresh__BackingField;

	public string LongName
	{
		get
		{
			return LongName__BackingField;
		}
		set
		{
			LongName__BackingField = value;
		}
	}

	public string ShortName
	{
		get
		{
			return ShortName__BackingField;
		}
		set
		{
			ShortName__BackingField = value;
		}
	}

	public SECRET_AGENT_RIVAL_AGENCY_SECURITY_LEVEL SecurityLevel
	{
		get
		{
			return SecurityLevel__BackingField;
		}
		set
		{
			SecurityLevel__BackingField = value;
		}
	}

	public double BankBalance
	{
		get
		{
			return BankBalance__BackingField;
		}
		set
		{
			BankBalance__BackingField = value;
		}
	}

	public List<SimSecretAgentMission> MissionsList
	{
		get
		{
			return MissionsList__BackingField;
		}
		set
		{
			MissionsList__BackingField = value;
		}
	}

	public List<SimSecretAgent> AgentsList
	{
		get
		{
			return AgentsList__BackingField;
		}
		set
		{
			AgentsList__BackingField = value;
		}
	}

	public int AgeAtLastSecretAgentRefresh
	{
		get
		{
			return AgeAtLastSecretAgentRefresh__BackingField;
		}
		set
		{
			AgeAtLastSecretAgentRefresh__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	private void RefreshFunds()
	{
	}

	private void RefreshAgents()
	{
	}

	private void CheckForDeadAgents()
	{
	}

	private void RefreshMissions()
	{
	}

	public void AddAgent(SimSecretAgent agent)
	{
	}

	public void RemoveAgent(SimSecretAgent agent)
	{
	}

	public void AddMission(SimSecretAgentMission mission)
	{
	}

	public void RemoveMission(SimSecretAgentMission mission)
	{
	}

	public string GetSecurityLevelString()
	{
		return null;
	}

	public List<SimSecretAgent> GetInfiltrationAgents()
	{
		return null;
	}

	public List<SimSecretAgentMission> GetInfiltrationMissions()
	{
		return null;
	}

	public double GetInfiltrationFunds()
	{
		return 0.0;
	}
}
