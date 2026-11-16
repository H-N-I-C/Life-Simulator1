using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimSpecialJobModel : SimSpecialJob
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimOccupationSpecial, int> _003C_003E9__74_0;

		public static Comparison<SimModelGig> _003C_003E9__102_1;

		internal int _003CIncrementAge_003Eb__74_0(SimOccupationSpecial x)
		{
			return 0;
		}

		internal int _003CGetGigsList_003Eb__102_1(SimModelGig i, SimModelGig j)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass102_0
	{
		public MODEL_GIG_TYPE gigType;

		internal bool _003CGetGigsList_003Eb__0(SimModelGig t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass109_0
	{
		public MODEL_GIG_TYPE type;

		public Func<SimModelGig, bool> _003C_003E9__0;

		internal bool _003CGetGigCountFromAllPastJobs_003Eb__0(SimModelGig t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass110_0
	{
		public MODEL_GIG_TYPE type;

		internal bool _003CGetGigCountFromCurrentJob_003Eb__0(SimModelGig x)
		{
			return false;
		}

		internal bool _003CGetGigCountFromCurrentJob_003Eb__1(SimModelGig t)
		{
			return false;
		}
	}

	private MODEL_CAREER_TYPE ModelType__BackingField;

	private SimModelingAgency Agency__BackingField;

	private SimModelSignatureStyle SignatureStyle__BackingField;

	private double TotalEarnings__BackingField;

	private List<SimModelGig> _PreviousGigsArray;

	[NonSerialized]
	public List<SimModelGig> _CurrentGigsArray;

	[NonSerialized]
	public List<Scenario> _ModelIncrementAgeScenarioQueueArray;

	private int AgeAtLastGigRefresh__BackingField;

	private int AgeAtStartRole__BackingField;

	private int AgeAtLastPractice__BackingField;

	private int NumPracticesAtCurrentAge__BackingField;

	private int AgeAtLastPublicityStunt__BackingField;

	private float StressScore__BackingField;

	private int AgeAtLastModelInteraction__BackingField;

	private int AgeAtLastMagazineCoverOffer__BackingField;

	private float Att_performance__BackingField;

	private float Att_reputation__BackingField;

	private float Att_profile__BackingField;

	public MODEL_CAREER_TYPE ModelType
	{
		get
		{
			return ModelType__BackingField;
		}
		set
		{
			ModelType__BackingField = value;
		}
	}

	public SimModelingAgency Agency
	{
		get
		{
			return Agency__BackingField;
		}
		set
		{
			Agency__BackingField = value;
		}
	}

	public SimModelSignatureStyle SignatureStyle
	{
		get
		{
			return SignatureStyle__BackingField;
		}
		set
		{
			SignatureStyle__BackingField = value;
		}
	}

	public double TotalEarnings
	{
		get
		{
			return TotalEarnings__BackingField;
		}
		set
		{
			TotalEarnings__BackingField = value;
		}
	}

	public List<SimModelGig> PreviousGigsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimModelGig> CurrentGigsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<Scenario> ModelIncrementAgeScenarioQueueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastGigRefresh
	{
		get
		{
			return AgeAtLastGigRefresh__BackingField;
		}
		set
		{
			AgeAtLastGigRefresh__BackingField = value;
		}
	}

	public int AgeAtStartRole
	{
		get
		{
			return AgeAtStartRole__BackingField;
		}
		set
		{
			AgeAtStartRole__BackingField = value;
		}
	}

	public int AgeAtLastPractice
	{
		get
		{
			return AgeAtLastPractice__BackingField;
		}
		set
		{
			AgeAtLastPractice__BackingField = value;
		}
	}

	public int NumPracticesAtCurrentAge
	{
		get
		{
			return NumPracticesAtCurrentAge__BackingField;
		}
		set
		{
			NumPracticesAtCurrentAge__BackingField = value;
		}
	}

	public int AgeAtLastPublicityStunt
	{
		get
		{
			return AgeAtLastPublicityStunt__BackingField;
		}
		set
		{
			AgeAtLastPublicityStunt__BackingField = value;
		}
	}

	public float StressScore
	{
		get
		{
			return StressScore__BackingField;
		}
		set
		{
			StressScore__BackingField = value;
		}
	}

	public int AgeAtLastModelInteraction
	{
		get
		{
			return AgeAtLastModelInteraction__BackingField;
		}
		set
		{
			AgeAtLastModelInteraction__BackingField = value;
		}
	}

	public int AgeAtLastMagazineCoverOffer
	{
		get
		{
			return AgeAtLastMagazineCoverOffer__BackingField;
		}
		set
		{
			AgeAtLastMagazineCoverOffer__BackingField = value;
		}
	}

	public float Att_performance
	{
		get
		{
			return Att_performance__BackingField;
		}
		set
		{
			Att_performance__BackingField = value;
		}
	}

	public float Att_reputation
	{
		get
		{
			return Att_reputation__BackingField;
		}
		set
		{
			Att_reputation__BackingField = value;
		}
	}

	public float Att_profile
	{
		get
		{
			return Att_profile__BackingField;
		}
		set
		{
			Att_profile__BackingField = value;
		}
	}

	public SimSpecialJobModel(SimModelingAgency agency)
	{
	}

	public void SetupEverythingElseForNewSpecialJobModel()
	{
	}

	public override void IncrementAge()
	{
	}

	private void CheckForPromotion()
	{
	}

	public void RefreshAgent()
	{
	}

	public override string JobTitleWithArticle(bool article)
	{
		return null;
	}

	public override bool ShouldBecomeFamous()
	{
		return false;
	}

	public override int GetMaxFame()
	{
		return 0;
	}

	public override int GetFameType()
	{
		return 0;
	}

	public override bool CanAttendRehab()
	{
		return false;
	}

	public static string GetModelFameTitle()
	{
		return null;
	}

	public static SimSpecialJobModel GetCurrentSpecialJobModel()
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialOccupationsAsModel()
	{
		return null;
	}

	public static SimSpecialJobModel GetMostRecentSpecialJobModel()
	{
		return null;
	}

	public static int GetYearsSinceLastRole()
	{
		return 0;
	}

	public float GetStartingPerformance()
	{
		return 0f;
	}

	private float GetStartReputation()
	{
		return 0f;
	}

	private float GetStartProfile()
	{
		return 0f;
	}

	public static float GetRoundedCompensation(float compensation)
	{
		return 0f;
	}

	public float GetAverageClientReputation()
	{
		return 0f;
	}

	public float GetAverageReputationForColleagues()
	{
		return 0f;
	}

	public SimBookingAgent GetCurrentBookingAgent()
	{
		return null;
	}

	public string GetOccupationString()
	{
		return null;
	}

	public void SetupLawsuitTargetAgainstStudioForDisease(SimDisease disease, string clientName)
	{
	}

	public void ClearAnyPendingModelScenarios()
	{
	}

	public void ImpactPerformance(int impactScore)
	{
	}

	public void ImpactProfile(int impactScore)
	{
	}

	public void ImpactReputation(int impactScore)
	{
	}

	public static bool DevForceModelOffers()
	{
		return false;
	}

	public SimModelGig GenerateNewGig(MODEL_GIG_TYPE type, SimModelingAgencyClient client = null)
	{
		return null;
	}

	public static List<SimModelGig> GetGigsList(MODEL_GIG_TYPE gigType)
	{
		return null;
	}

	public void RefreshGigs()
	{
	}

	public static string GetIndustryString(MODEL_INDUSTRY industry)
	{
		return null;
	}

	public static string GetGigCampaignTypeString(MODEL_CAMPAIGN_TYPE campaignType, bool addIndefiniteArticle)
	{
		return null;
	}

	public static string GetModelCareerTypeString(MODEL_CAREER_TYPE careerType, bool addIndefiniteArticle)
	{
		return null;
	}

	public static SimSpecialJobModel GenerateNewSimSpecialJobOrGetPrevious()
	{
		return null;
	}

	public void TerminateJob(SPECIAL_JOB_TERMINATION_TYPE type)
	{
	}

	public static int GetGigCountFromAllPastJobs(MODEL_GIG_TYPE type = MODEL_GIG_TYPE.NONE)
	{
		return 0;
	}

	public static int GetGigCountFromCurrentJob(MODEL_GIG_TYPE type = MODEL_GIG_TYPE.NONE)
	{
		return 0;
	}

	public static bool HasAchievedEliteModelStatus()
	{
		return false;
	}

	public static bool CanAgentFindGigForHero()
	{
		return false;
	}
}
