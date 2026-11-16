using System;

[Serializable]
public class SimMartialArt
{
	public string MartialArtName;

	private int BeltIndexNI__BackingField;

	public DataMartialArt Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int BeltIndexNI
	{
		get
		{
			return BeltIndexNI__BackingField;
		}
		set
		{
			BeltIndexNI__BackingField = value;
		}
	}

	public string GetCurrentBeltString()
	{
		return null;
	}

	public bool HasEarnedMove1()
	{
		return false;
	}

	public bool HasEarnedMove2()
	{
		return false;
	}
}
