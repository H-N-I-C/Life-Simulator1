using System;
using System.Collections.Generic;
using Genesis.Controllers;

[Serializable]
public class SimSeniorCenter
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<SimSeniorPerson> _003C_003E9__15_0;

		public static Func<SimSeniorPerson, bool> _003C_003E9__16_0;

		public static Func<SimSeniorPerson, bool> _003C_003E9__16_1;

		internal bool _003CEnsureSeniorNpcList_003Eb__15_0(SimSeniorPerson senior)
		{
			return false;
		}

		internal bool _003CSelectRandomSeniorForDrama_003Eb__16_0(SimSeniorPerson m)
		{
			return false;
		}

		internal bool _003CSelectRandomSeniorForDrama_003Eb__16_1(SimSeniorPerson m)
		{
			return false;
		}
	}

	private const int DefaultSeniorCount = 10;

	private List<SimSeniorPerson> SeniorNpcList__BackingField;

	[NonSerialized]
	private SimSeniorPerson _dramaTarget;

	[NonSerialized]
	private SimPerson _swinger;

	[NonSerialized]
	private SimPerson _swingerLover;

	public List<SimSeniorPerson> SeniorNpcList
	{
		get
		{
			return SeniorNpcList__BackingField;
		}
		set
		{
			SeniorNpcList__BackingField = value;
		}
	}

	public static void Init()
	{
	}

	public void ClearSeniorNpcList()
	{
	}

	public void IncrementAge()
	{
	}

	public List<SimSeniorPerson> GetSeniorCenterList(int count = 10)
	{
		return null;
	}

	public int GetAverageRelationshipStrength()
	{
		return 0;
	}

	private static float GetBaseRelationshipStrength(SimSeniorPerson senior)
	{
		return 0f;
	}

	private void EnsureSeniorNpcList(int count = 10)
	{
	}

	public string SelectRandomSeniorForDrama()
	{
		return null;
	}

	public string GenerateSwingPartners()
	{
		return null;
	}

	public string GenerateSwingPartnersFromGenesis(ScenarioController controller)
	{
		return null;
	}

	public SimPerson GetSwinger()
	{
		return null;
	}

	public SimPerson GetSwingerLover()
	{
		return null;
	}

	private void SetSwingPartnerMemory(SimPerson swinger, SimPerson swingerLover)
	{
	}

	private void SetDramaTargetMemory(SimSeniorPerson target, bool hasNonFriend)
	{
	}

	public void ShowGenesisSeniorCenterActivity()
	{
	}

	public bool ShouldShowGenesisSeniorCenterList()
	{
		return false;
	}

	public Scenario GetGenesisSeniorCenterFeedbackScenario()
	{
		return null;
	}

	public ListItem BuildSeniorCenterActivityItem()
	{
		return null;
	}
}
