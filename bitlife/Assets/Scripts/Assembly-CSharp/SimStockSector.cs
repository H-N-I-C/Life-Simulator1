using System;
using System.Collections.Generic;

[Serializable]
public class SimStockSector
{
	public string Name;

	public int Sector;

	private List<SimSectorEvent> SectorEventList__BackingField;

	private List<SimStock> StockList__BackingField;

	public int CurrentState;

	private bool HasABankruptStock__BackingField;

	public List<SimSectorEvent> SectorEventList
	{
		get
		{
			return SectorEventList__BackingField;
		}
		set
		{
			SectorEventList__BackingField = value;
		}
	}

	public List<SimStock> StockList
	{
		get
		{
			return StockList__BackingField;
		}
		set
		{
			StockList__BackingField = value;
		}
	}

	public bool HasABankruptStock
	{
		get
		{
			return HasABankruptStock__BackingField;
		}
		set
		{
			HasABankruptStock__BackingField = value;
		}
	}
}
