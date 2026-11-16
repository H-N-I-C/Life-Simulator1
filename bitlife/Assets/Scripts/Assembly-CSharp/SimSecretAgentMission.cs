using System;

[Serializable]
public class SimSecretAgentMission
{
	private sealed class _003C_003Ec__DisplayClass49_0
	{
		public string name;

		internal bool _003CGadgetFromName_003Eb__0(SimSecretAgentGadget x)
		{
			return false;
		}
	}

	private string CodeName__BackingField;

	private string Prefix__BackingField;

	private SimSecretAgent AssignedAgent__BackingField;

	private SimSecretAgentGadget EquippedGadget__BackingField;

	private int Risk__BackingField;

	private bool Success__BackingField;

	private SECRET_AGENT_SPECIALIZATION Specialization__BackingField;

	private double PayOut__BackingField;

	private int KillCount__BackingField;

	private SimSecretAgentMissionClient Client__BackingField;

	private SimSecretAgentGadget IAPGadget__BackingField;

	private int HeroAgeAtComplete__BackingField;

	public string CodeName
	{
		get
		{
			return CodeName__BackingField;
		}
		set
		{
			CodeName__BackingField = value;
		}
	}

	public string Prefix
	{
		get
		{
			return Prefix__BackingField;
		}
		set
		{
			Prefix__BackingField = value;
		}
	}

	public SimSecretAgent AssignedAgent
	{
		get
		{
			return AssignedAgent__BackingField;
		}
		set
		{
			AssignedAgent__BackingField = value;
		}
	}

	public SimSecretAgentGadget EquippedGadget
	{
		get
		{
			return EquippedGadget__BackingField;
		}
		set
		{
			EquippedGadget__BackingField = value;
		}
	}

	public int Risk
	{
		get
		{
			return Risk__BackingField;
		}
		set
		{
			Risk__BackingField = value;
		}
	}

	public bool Success
	{
		get
		{
			return Success__BackingField;
		}
		set
		{
			Success__BackingField = value;
		}
	}

	public SECRET_AGENT_SPECIALIZATION Specialization
	{
		get
		{
			return Specialization__BackingField;
		}
		set
		{
			Specialization__BackingField = value;
		}
	}

	public double PayOut
	{
		get
		{
			return PayOut__BackingField;
		}
		set
		{
			PayOut__BackingField = value;
		}
	}

	public int KillCount
	{
		get
		{
			return KillCount__BackingField;
		}
		set
		{
			KillCount__BackingField = value;
		}
	}

	public SimSecretAgentMissionClient Client
	{
		get
		{
			return Client__BackingField;
		}
		set
		{
			Client__BackingField = value;
		}
	}

	public SimSecretAgentGadget IAPGadget
	{
		get
		{
			return IAPGadget__BackingField;
		}
		set
		{
			IAPGadget__BackingField = value;
		}
	}

	public int HeroAgeAtComplete
	{
		get
		{
			return HeroAgeAtComplete__BackingField;
		}
		set
		{
			HeroAgeAtComplete__BackingField = value;
		}
	}

	public SimSecretAgentGadget GadgetFromName(string name)
	{
		return null;
	}

	public string FullMissionName()
	{
		return null;
	}

	public bool IsSuccessfulMissionAction()
	{
		return false;
	}

	private int GetMissionAgentBuff()
	{
		return 0;
	}

	public string ClassificationString()
	{
		return null;
	}

	public int RiskForMission(SECRET_AGENT_SPECIALIZATION specialization, int targetRiskScore)
	{
		return 0;
	}

	public int MaxTargetRiskScoreAllowed()
	{
		return 0;
	}

	public double PayoutForRisk(int risk)
	{
		return 0.0;
	}

	public void PostMissionImpacts(bool successfulMission)
	{
	}

	protected string GetRandomPrefix()
	{
		return null;
	}

	public string PronounStringForType(int gender, int type)
	{
		return null;
	}

	public bool IAPGadgetIsSuccessful()
	{
		return false;
	}

	public string MissionObjectiveString()
	{
		return null;
	}
}
