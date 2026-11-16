using System;
using System.Collections.Generic;

[Serializable]
public class SimDealerDrugCut : SimDealerDrug
{
	private string _sourceDrugType;

	private string CutName__BackingField;

	private int CutIngredientPercentage__BackingField;

	private string CutIngredient__BackingField;

	private bool CreatedByHero__BackingField;

	private List<string> _cutAdditionalEffects;

	public string CutName
	{
		get
		{
			return CutName__BackingField;
		}
		set
		{
			CutName__BackingField = value;
		}
	}

	public int CutIngredientPercentage
	{
		get
		{
			return CutIngredientPercentage__BackingField;
		}
		set
		{
			CutIngredientPercentage__BackingField = value;
		}
	}

	public string CutIngredient
	{
		get
		{
			return CutIngredient__BackingField;
		}
		set
		{
			CutIngredient__BackingField = value;
		}
	}

	public bool CreatedByHero
	{
		get
		{
			return CreatedByHero__BackingField;
		}
		set
		{
			CreatedByHero__BackingField = value;
		}
	}

	public SimDealerDrugCut(SimDealerDrug sourceDrug, string cutIngredient, int cutIngredientPercentage, string cutName, int doses, bool pullFromSourceDrugSupply = true)
		: base(null, 0.0, 0)
	{
	}

	public override string GetEffectsListString()
	{
		return null;
	}

	private SimDealerDrug GetSourceDrug()
	{
		return null;
	}
}
