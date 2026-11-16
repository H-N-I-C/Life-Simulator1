using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimModelingAgency
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataModelCategory, string> _003C_003E9__61_0;

		public static Func<SimModelingAgencyClient, bool> _003C_003E9__66_0;

		public static Func<SimModelingAgencyClient, bool> _003C_003E9__69_0;

		public static Func<SimModel, bool> _003C_003E9__70_0;

		public static Func<SimModel, string> _003C_003E9__70_1;

		public static Func<SimModelingAgencyClient, string> _003C_003E9__71_0;

		public static Func<SimModelingAgencyClient, int> _003C_003E9__72_0;

		public static Func<SimModelingAgencyClient, float> _003C_003E9__72_1;

		internal string _003CGenerateNewClient_003Eb__61_0(DataModelCategory t)
		{
			return null;
		}

		internal bool _003CGetMajorClientsList_003Eb__66_0(SimModelingAgencyClient x)
		{
			return false;
		}

		internal bool _003CGetFashionWeekEligibleClients_003Eb__69_0(SimModelingAgencyClient x)
		{
			return false;
		}

		internal bool _003CGetFamousModelsListCount_003Eb__70_0(SimModel x)
		{
			return false;
		}

		internal string _003CGetFamousModelsListCount_003Eb__70_1(SimModel x)
		{
			return null;
		}

		internal string _003CGetMajorClientListString_003Eb__71_0(SimModelingAgencyClient x)
		{
			return null;
		}

		internal int _003CRefreshClientList_003Eb__72_0(SimModelingAgencyClient t)
		{
			return 0;
		}

		internal float _003CRefreshClientList_003Eb__72_1(SimModelingAgencyClient t)
		{
			return 0f;
		}
	}

	private sealed class _003C_003Ec__DisplayClass60_0
	{
		public SimModelingAgencyClient client;

		internal bool _003C_002Ector_003Eb__0(SimModelingAgencyClient t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass60_1
	{
		public SimModelingAgencyClient clientFashion;

		internal bool _003C_002Ector_003Eb__1(SimModelingAgencyClient client)
		{
			return false;
		}
	}

	private List<SimModelingAgencyClient> ClientList__BackingField;

	private List<string> CelebrityModelList__BackingField;

	private List<SimModel> ModelList__BackingField;

	private SimBookingAgent BookingAgent__BackingField;

	private string Name__BackingField;

	private float Commission__BackingField;

	private float Profile__BackingField;

	private int TypicalGender__BackingField;

	private int MinAge__BackingField;

	private int MaxAge__BackingField;

	private int YearsAtAgency__BackingField;

	private int NumCampaigns__BackingField;

	private int LastHeroAgeAgencyUpdate__BackingField;

	private int NumGoSeesAtCurrentAge__BackingField;

	private int HeroAgeUpdateClientList__BackingField;

	public List<SimModelingAgencyClient> ClientList
	{
		get
		{
			return ClientList__BackingField;
		}
		set
		{
			ClientList__BackingField = value;
		}
	}

	public List<string> CelebrityModelList
	{
		get
		{
			return CelebrityModelList__BackingField;
		}
		set
		{
			CelebrityModelList__BackingField = value;
		}
	}

	public List<SimModel> ModelList
	{
		get
		{
			return ModelList__BackingField;
		}
		set
		{
			ModelList__BackingField = value;
		}
	}

	public SimBookingAgent BookingAgent
	{
		get
		{
			return BookingAgent__BackingField;
		}
		set
		{
			BookingAgent__BackingField = value;
		}
	}

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

	public float Commission
	{
		get
		{
			return Commission__BackingField;
		}
		set
		{
			Commission__BackingField = value;
		}
	}

	public float Profile
	{
		get
		{
			return Profile__BackingField;
		}
		set
		{
			Profile__BackingField = value;
		}
	}

	public int TypicalGender
	{
		get
		{
			return TypicalGender__BackingField;
		}
		set
		{
			TypicalGender__BackingField = value;
		}
	}

	public int MinAge
	{
		get
		{
			return MinAge__BackingField;
		}
		set
		{
			MinAge__BackingField = value;
		}
	}

	public int MaxAge
	{
		get
		{
			return MaxAge__BackingField;
		}
		set
		{
			MaxAge__BackingField = value;
		}
	}

	public int YearsAtAgency
	{
		get
		{
			return YearsAtAgency__BackingField;
		}
		set
		{
			YearsAtAgency__BackingField = value;
		}
	}

	public int NumCampaigns
	{
		get
		{
			return NumCampaigns__BackingField;
		}
		set
		{
			NumCampaigns__BackingField = value;
		}
	}

	public int LastHeroAgeAgencyUpdate
	{
		get
		{
			return LastHeroAgeAgencyUpdate__BackingField;
		}
		set
		{
			LastHeroAgeAgencyUpdate__BackingField = value;
		}
	}

	public int NumGoSeesAtCurrentAge
	{
		get
		{
			return NumGoSeesAtCurrentAge__BackingField;
		}
		set
		{
			NumGoSeesAtCurrentAge__BackingField = value;
		}
	}

	public int HeroAgeUpdateClientList
	{
		get
		{
			return HeroAgeUpdateClientList__BackingField;
		}
		set
		{
			HeroAgeUpdateClientList__BackingField = value;
		}
	}

	private SimModelingAgencyClient GenerateNewClient(MODEL_CAREER_TYPE modelType, MODEL_INDUSTRY industry = MODEL_INDUSTRY.NONE)
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	private void RefreshModelList()
	{
	}

	public void CheckDeadModels()
	{
	}

	public SimModelingAgencyClient GetRandomClient()
	{
		return null;
	}

	public List<SimModelingAgencyClient> GetMajorClientsList()
	{
		return null;
	}

	public void ImpactRelationshipWithAllClients(int impactScore)
	{
	}

	public bool HasEnoughFashionWeekEligibleClients()
	{
		return false;
	}

	public List<SimModelingAgencyClient> GetFashionWeekEligibleClients()
	{
		return null;
	}

	public int GetFamousModelsListCount()
	{
		return 0;
	}

	public string GetMajorClientListString()
	{
		return null;
	}

	private void RefreshClientList()
	{
	}

	public SimModel GetRandomModel()
	{
		return null;
	}
}
