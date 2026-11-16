using System;

[Serializable]
public class ChoiceCurrencyDataItem
{
	private string CurrencyId__BackingField;

	private string CostAmount__BackingField;

	public string CurrencyId
	{
		get
		{
			return CurrencyId__BackingField;
		}
		set
		{
			CurrencyId__BackingField = value;
		}
	}

	public string CostAmount
	{
		get
		{
			return CostAmount__BackingField;
		}
		set
		{
			CostAmount__BackingField = value;
		}
	}
}
