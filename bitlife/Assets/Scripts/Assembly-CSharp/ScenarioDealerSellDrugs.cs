using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioDealerSellDrugs : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimChild, bool> _003C_003E9__10_0;

		public static Func<SimSibling, bool> _003C_003E9__10_1;

		public static Func<SimPerson, bool> _003C_003E9__10_2;

		public static Func<SimChild, bool> _003C_003E9__10_3;

		internal bool _003CGetPersonList_003Eb__10_0(SimChild t)
		{
			return false;
		}

		internal bool _003CGetPersonList_003Eb__10_1(SimSibling t)
		{
			return false;
		}

		internal bool _003CGetPersonList_003Eb__10_2(SimPerson t)
		{
			return false;
		}

		internal bool _003CGetPersonList_003Eb__10_3(SimChild t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private List<SimPerson> _persons;

	private ArrayList _personDropDownContents;

	private List<SimDealerDrug> _drugs;

	private ArrayList _drugDropDownContents;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private string RelationshipString(SimPerson person)
	{
		return null;
	}

	public static List<SimPerson> GetPersonList()
	{
		return null;
	}
}
