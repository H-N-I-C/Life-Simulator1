using System;

public class DataGift : IComparable
{
	private string Name__BackingField;

	private bool IsSingular__BackingField;

	private bool UseThem__BackingField;

	private bool IsFlowers__BackingField;

	private bool CanBeShoplifted__BackingField;

	private int Cost__BackingField;

	private int MaleAppreciationScore__BackingField;

	private int FemaleAppreciationScore__BackingField;

	private int MinAge__BackingField;

	private int MaxAge__BackingField;

	private int CommonnessScore__BackingField;

	private bool IsSantaSack__BackingField;

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

	public bool IsSingular
	{
		get
		{
			return IsSingular__BackingField;
		}
		set
		{
			IsSingular__BackingField = value;
		}
	}

	public bool UseThem
	{
		get
		{
			return UseThem__BackingField;
		}
		set
		{
			UseThem__BackingField = value;
		}
	}

	public bool IsFlowers
	{
		get
		{
			return IsFlowers__BackingField;
		}
		set
		{
			IsFlowers__BackingField = value;
		}
	}

	public bool CanBeShoplifted
	{
		get
		{
			return CanBeShoplifted__BackingField;
		}
		set
		{
			CanBeShoplifted__BackingField = value;
		}
	}

	public int Cost
	{
		get
		{
			return Cost__BackingField;
		}
		set
		{
			Cost__BackingField = value;
		}
	}

	public int MaleAppreciationScore
	{
		get
		{
			return MaleAppreciationScore__BackingField;
		}
		set
		{
			MaleAppreciationScore__BackingField = value;
		}
	}

	public int FemaleAppreciationScore
	{
		get
		{
			return FemaleAppreciationScore__BackingField;
		}
		set
		{
			FemaleAppreciationScore__BackingField = value;
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

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public bool IsSantaSack
	{
		get
		{
			return IsSantaSack__BackingField;
		}
		set
		{
			IsSantaSack__BackingField = value;
		}
	}

	public string GetNameWithArticle()
	{
		return null;
	}

	public bool IsBitizenship()
	{
		return false;
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
