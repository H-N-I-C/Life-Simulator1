using System;

[Serializable]
public class GenderedPart : SimVampireHonorificPart
{
	private string MaleSpecific__BackingField;

	private string FemaleSpecific__BackingField;

	private string BothSpecific__BackingField;

	public string MaleSpecific
	{
		get
		{
			return MaleSpecific__BackingField;
		}
		set
		{
			MaleSpecific__BackingField = value;
		}
	}

	public string FemaleSpecific
	{
		get
		{
			return FemaleSpecific__BackingField;
		}
		set
		{
			FemaleSpecific__BackingField = value;
		}
	}

	public string BothSpecific
	{
		get
		{
			return BothSpecific__BackingField;
		}
		set
		{
			BothSpecific__BackingField = value;
		}
	}

	public override string GetPart(SimPerson person)
	{
		return null;
	}
}
