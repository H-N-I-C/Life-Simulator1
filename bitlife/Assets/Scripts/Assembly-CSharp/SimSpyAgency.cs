using System;
using System.Collections.Generic;

[Serializable]
public class SimSpyAgency
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSecretAgentMission, bool> _003C_003E9__95_0;

		public static Func<SimSecretAgentMission, bool> _003C_003E9__95_1;

		public static Func<SimSecretAgent, int> _003C_003E9__95_2;

		public static Func<SimSecretAgent, bool> _003C_003E9__95_3;

		public static Func<SimSecretAgent, bool> _003C_003E9__95_6;

		public static Func<SimSecretAgent, bool> _003C_003E9__95_7;

		public static Func<SimSecretAgentMission, bool> _003C_003E9__110_0;

		public static Func<SimSecretAgentGadget, int> _003C_003E9__124_1;

		public static Func<SimSecretAgent, bool> _003C_003E9__127_0;

		internal bool _003CIncrementAge_003Eb__95_0(SimSecretAgentMission t)
		{
			return false;
		}

		internal bool _003CIncrementAge_003Eb__95_1(SimSecretAgentMission t)
		{
			return false;
		}

		internal int _003CIncrementAge_003Eb__95_2(SimSecretAgent t)
		{
			return 0;
		}

		internal bool _003CIncrementAge_003Eb__95_3(SimSecretAgent t)
		{
			return false;
		}

		internal bool _003CIncrementAge_003Eb__95_6(SimSecretAgent t)
		{
			return false;
		}

		internal bool _003CIncrementAge_003Eb__95_7(SimSecretAgent t)
		{
			return false;
		}

		internal bool _003CGetPerformance_003Eb__110_0(SimSecretAgentMission x)
		{
			return false;
		}

		internal int _003CGetGadgetDropDownList_003Eb__124_1(SimSecretAgentGadget t)
		{
			return 0;
		}

		internal bool _003CCheckSpecialOperativesAchievement_003Eb__127_0(SimSecretAgent t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass124_0
	{
		public SimSecretAgentGadget item;

		internal bool _003CGetGadgetDropDownList_003Eb__0(SimSecretAgentGadget t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass94_0
	{
		public SimRivalSpyAgency rivalAgency;

		internal bool _003CSetupAgency_003Eb__0(SimRivalSpyAgency t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass95_0
	{
		public SimSecretAgent recoverAgent;

		internal bool _003CIncrementAge_003Eb__4(SimSecretAgentMission x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass95_1
	{
		public SimSecretAgent spottedAgent;

		internal bool _003CIncrementAge_003Eb__5(SimSecretAgentMission x)
		{
			return false;
		}
	}

	private string Name__BackingField;

	private string HeroCodeName__BackingField;

	private float HeroPerformanceIndex__BackingField;

	private int Age__BackingField;

	private float Prestige__BackingField;

	private bool HasReceivedBankruptcyWarning__BackingField;

	private bool Bankrupt__BackingField;

	private int NumViewableRecruitsAvailableThisYear__BackingField;

	private int NumSecurityTechniciansAvailableThisYear__BackingField;

	private int NumAttemptsToInfiltrateThisYear__BackingField;

	private int NumTimesTriedSpyOnPeople__BackingField;

	private SimSpyAgencyFront Front__BackingField;

	private List<SimSecretAgent> ExAgentsList__BackingField;

	private List<SimSecretAgentMission> AvailableMissionList__BackingField;

	private List<SimSecretAgentMission> CompletedMissionList__BackingField;

	private List<SimRivalSpyAgency> RivalAgenciesList__BackingField;

	private List<SimSpyAgencySecurityFeature> SecurityFeaturesList__BackingField;

	private List<SimSecretAgentGadget> GadgetsList__BackingField;

	private List<SimSecretAgentGadget> MarketplaceGadgetList__BackingField;

	private List<SimSecretAgent> _agentsList;

	[NonSerialized]
	private List<Scenario> _SecretAgentIncrementAgeScenarioQueueArray;

	private int NumExposedDoubleAgents__BackingField;

	private int NumTaxidermiedAgents__BackingField;

	private int NumAgentsMurderedByHero__BackingField;

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

	public string HeroCodeName
	{
		get
		{
			return HeroCodeName__BackingField;
		}
		set
		{
			HeroCodeName__BackingField = value;
		}
	}

	public float HeroPerformanceIndex
	{
		get
		{
			return HeroPerformanceIndex__BackingField;
		}
		set
		{
			HeroPerformanceIndex__BackingField = value;
		}
	}

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public float Prestige
	{
		get
		{
			return Prestige__BackingField;
		}
		set
		{
			Prestige__BackingField = value;
		}
	}

	public bool HasReceivedBankruptcyWarning
	{
		get
		{
			return HasReceivedBankruptcyWarning__BackingField;
		}
		set
		{
			HasReceivedBankruptcyWarning__BackingField = value;
		}
	}

	public bool Bankrupt
	{
		get
		{
			return Bankrupt__BackingField;
		}
		set
		{
			Bankrupt__BackingField = value;
		}
	}

	public int NumViewableRecruitsAvailableThisYear
	{
		get
		{
			return NumViewableRecruitsAvailableThisYear__BackingField;
		}
		set
		{
			NumViewableRecruitsAvailableThisYear__BackingField = value;
		}
	}

	public int NumSecurityTechniciansAvailableThisYear
	{
		get
		{
			return NumSecurityTechniciansAvailableThisYear__BackingField;
		}
		set
		{
			NumSecurityTechniciansAvailableThisYear__BackingField = value;
		}
	}

	public int NumAttemptsToInfiltrateThisYear
	{
		get
		{
			return NumAttemptsToInfiltrateThisYear__BackingField;
		}
		set
		{
			NumAttemptsToInfiltrateThisYear__BackingField = value;
		}
	}

	public int NumTimesTriedSpyOnPeople
	{
		get
		{
			return NumTimesTriedSpyOnPeople__BackingField;
		}
		set
		{
			NumTimesTriedSpyOnPeople__BackingField = value;
		}
	}

	public SimSpyAgencyFront Front
	{
		get
		{
			return Front__BackingField;
		}
		set
		{
			Front__BackingField = value;
		}
	}

	public List<SimSecretAgent> ExAgentsList
	{
		get
		{
			return ExAgentsList__BackingField;
		}
		set
		{
			ExAgentsList__BackingField = value;
		}
	}

	public List<SimSecretAgentMission> AvailableMissionList
	{
		get
		{
			return AvailableMissionList__BackingField;
		}
		set
		{
			AvailableMissionList__BackingField = value;
		}
	}

	public List<SimSecretAgentMission> CompletedMissionList
	{
		get
		{
			return CompletedMissionList__BackingField;
		}
		set
		{
			CompletedMissionList__BackingField = value;
		}
	}

	public List<SimRivalSpyAgency> RivalAgenciesList
	{
		get
		{
			return RivalAgenciesList__BackingField;
		}
		set
		{
			RivalAgenciesList__BackingField = value;
		}
	}

	public List<SimSpyAgencySecurityFeature> SecurityFeaturesList
	{
		get
		{
			return SecurityFeaturesList__BackingField;
		}
		set
		{
			SecurityFeaturesList__BackingField = value;
		}
	}

	public List<SimSecretAgentGadget> GadgetsList
	{
		get
		{
			return GadgetsList__BackingField;
		}
		set
		{
			GadgetsList__BackingField = value;
		}
	}

	public List<SimSecretAgentGadget> MarketplaceGadgetList
	{
		get
		{
			return MarketplaceGadgetList__BackingField;
		}
		set
		{
			MarketplaceGadgetList__BackingField = value;
		}
	}

	public List<Scenario> SecretAgentIncrementAgeScenarioQueueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int NumExposedDoubleAgents
	{
		get
		{
			return NumExposedDoubleAgents__BackingField;
		}
		set
		{
			NumExposedDoubleAgents__BackingField = value;
		}
	}

	public int NumTaxidermiedAgents
	{
		get
		{
			return NumTaxidermiedAgents__BackingField;
		}
		set
		{
			NumTaxidermiedAgents__BackingField = value;
		}
	}

	public int NumAgentsMurderedByHero
	{
		get
		{
			return NumAgentsMurderedByHero__BackingField;
		}
		set
		{
			NumAgentsMurderedByHero__BackingField = value;
		}
	}

	public void SetupAgency()
	{
	}

	public void IncrementAge()
	{
	}

	private bool ProcessLoan(SimLoan loan)
	{
		return false;
	}

	public void AddAgent(SimSecretAgent agent)
	{
	}

	public void RemoveAgent(SimSecretAgent agent)
	{
	}

	public void HireAgent(SimSecretAgent agent)
	{
	}

	public void FireAgent(SimSecretAgent agent)
	{
	}

	public void AgentQuits(SimSecretAgent agent)
	{
	}

	public void AddMission(SimSecretAgentMission mission)
	{
	}

	public void RemoveMission(SimSecretAgentMission mission)
	{
	}

	public void AddSecurityFeature(SimSpyAgencySecurityFeature feature)
	{
	}

	public void RemoveSecurityFeature(SimSpyAgencySecurityFeature feature)
	{
	}

	public void AddGadget(SimSecretAgentGadget gadget)
	{
	}

	public void RemoveGadget(SimSecretAgentGadget gadget)
	{
	}

	public void ImpactPrestige(int impact)
	{
	}

	public void ImpactHeroPerformance(int impact)
	{
	}

	public float GetPerformance()
	{
		return 0f;
	}

	public float GetAverageAgentProficiency()
	{
		return 0f;
	}

	public void ShutDownAgency()
	{
	}

	public SimSecretAgent GenerateRecruitmentCandidate()
	{
		return null;
	}

	public SimSecretAgent GetRandomAgent()
	{
		return null;
	}

	public SimSecretAgent GetRandomRivalAgent()
	{
		return null;
	}

	public SimRivalSpyAgency GetRivalAgencyForAgent(SimSecretAgent agent)
	{
		return null;
	}

	public List<SimSecretAgent> GetMissingAgentsList()
	{
		return null;
	}

	public List<SimSecretAgent> GetActiveAgentsList()
	{
		return null;
	}

	public int GetRank()
	{
		return 0;
	}

	public static SimSecretAgentMission GenerateRandomMission(SECRET_AGENT_SPECIALIZATION specialization = SECRET_AGENT_SPECIALIZATION.NONE)
	{
		return null;
	}

	public List<SimSecretAgentGadget> GenerateNewMarketplaceGadgetList()
	{
		return null;
	}

	public void ImpactRelationshipStrengthWithAllActiveAgents(int impact)
	{
	}

	private void RefreshMissions()
	{
	}

	public List<SimSecretAgentGadget> GetGadgetDropDownList(SECRET_AGENT_SPECIALIZATION specialization)
	{
		return null;
	}

	public void CancelAllSecretAgentScenarios()
	{
	}

	public void MissionComplete(SimSecretAgentMission mission)
	{
	}

	public void CheckSpecialOperativesAchievement()
	{
	}

	private void CheckForDeadAgents(List<SimSecretAgent> agents)
	{
	}
}
