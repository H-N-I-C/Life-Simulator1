using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioDealerDealItem : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDealerDrug, float> _003C_003E9__23_0;

		internal float _003CRefreshScenario_003Eb__23_0(SimDealerDrug t)
		{
			return 0f;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimLover _exlover;

	protected SimChild _child;

	protected SimPerson _friend;

	protected SimPerson _person;

	protected SimClassmate _classmate;

	protected SimCoworker _coworker;

	protected string _personTypeString;

	protected string _dataPersonTypeString;

	private List<SimDealerDrug> _cartelDrugs;

	private Dictionary<string, DataDealerDealActivityItem> _items;

	private string _personNameScenario;

	public ScenarioDealerDealItem(SimParent theParent)
	{
	}

	public ScenarioDealerDealItem(SimSibling theSibling)
	{
	}

	public ScenarioDealerDealItem(SimLover theLover, bool isExLover = false)
	{
	}

	public ScenarioDealerDealItem(SimChild theChild)
	{
	}

	public ScenarioDealerDealItem(SimPerson theFriend)
	{
	}

	public ScenarioDealerDealItem(SimClassmate theClassmate)
	{
	}

	public ScenarioDealerDealItem(SimCoworker theCoworker)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
