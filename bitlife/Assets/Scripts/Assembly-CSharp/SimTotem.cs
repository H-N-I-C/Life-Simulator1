using System;
using Game;

[Serializable]
public class SimTotem
{
	private SimHeirloom Heirloom__BackingField;

	private SimAuctionItem AuctionItem__BackingField;

	private SimArtifact Artifact__BackingField;

	private SimJewel Jewel__BackingField;

	private SimModelGift ModelGift__BackingField;

	private string DefaultTotemString__BackingField;

	private int YearsAsTotem__BackingField;

	public SimHeirloom Heirloom
	{
		get
		{
			return Heirloom__BackingField;
		}
		set
		{
			Heirloom__BackingField = value;
		}
	}

	public SimAuctionItem AuctionItem
	{
		get
		{
			return AuctionItem__BackingField;
		}
		set
		{
			AuctionItem__BackingField = value;
		}
	}

	public SimArtifact Artifact
	{
		get
		{
			return Artifact__BackingField;
		}
		set
		{
			Artifact__BackingField = value;
		}
	}

	public SimJewel Jewel
	{
		get
		{
			return Jewel__BackingField;
		}
		set
		{
			Jewel__BackingField = value;
		}
	}

	public SimModelGift ModelGift
	{
		get
		{
			return ModelGift__BackingField;
		}
		set
		{
			ModelGift__BackingField = value;
		}
	}

	public DataCultDefaultTotem DefaultTotemData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string DefaultTotemString
	{
		get
		{
			return DefaultTotemString__BackingField;
		}
		set
		{
			DefaultTotemString__BackingField = value;
		}
	}

	public int YearsAsTotem
	{
		get
		{
			return YearsAsTotem__BackingField;
		}
		set
		{
			YearsAsTotem__BackingField = value;
		}
	}

	public SimTotem()
	{
	}

	public SimTotem(SimAuctionItem totem)
	{
	}

	public SimTotem(SimHeirloom totem)
	{
	}

	public SimTotem(SimJewel totem)
	{
	}

	public SimTotem(SimArtifact totem)
	{
	}

	public SimTotem(SimModelGift totem)
	{
	}

	public SimTotem(DataCultDefaultTotem defaultTotem)
	{
	}

	public void IncrementAge()
	{
	}

	public string PossessiveNameForTotem()
	{
		return null;
	}

	public string NameForTotem()
	{
		return null;
	}

	public string EmojiStringForTotem()
	{
		return null;
	}

	public float GetCondition()
	{
		return 0f;
	}

	public void ReturnItemToHero()
	{
	}

	public void SetTotem()
	{
	}

	public void ImpactCondition(int impactScore)
	{
	}

	public int GetCommonnessScore()
	{
		return 0;
	}

	public bool IsDefaultTotem()
	{
		return false;
	}
}
