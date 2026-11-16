using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimSpecialJobBusiness : SimSpecialJob
{
	private SimBusiness TheBusiness__BackingField;

	[NonSerialized]
	public List<Scenario> _BusinessIncrementAgeScenarioQueueArray;

	public SimBusiness TheBusiness
	{
		get
		{
			return TheBusiness__BackingField;
		}
		set
		{
			TheBusiness__BackingField = value;
		}
	}

	public List<Scenario> BusinessIncrementAgeScenarioQueueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimSpecialJobBusiness(SimBusiness theBusiness)
	{
	}

	public override void IncrementAge()
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

	public string GetOccupationString()
	{
		return null;
	}

	public void CancelRemainingBusinessScenarios()
	{
	}

	public void CancelRemainingIncrementAgeScenarios()
	{
	}

	public static SimSpecialJobBusiness GetCurrentSpecialJobBusiness()
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialOccupationsAsBusiness()
	{
		return null;
	}

	public static SimSpecialJobBusiness GetMostRecentSpecialJobBusiness()
	{
		return null;
	}

	public static void SetupSupplierForProductIfApplicable(SimProduct product)
	{
	}
}
