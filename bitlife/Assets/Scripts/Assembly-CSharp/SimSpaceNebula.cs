using System;

[Serializable]
public class SimSpaceNebula : SimDiscovery
{
	private string LooksLikeString__BackingField;

	private string Size__BackingField;

	public string LooksLikeString
	{
		get
		{
			return LooksLikeString__BackingField;
		}
		set
		{
			LooksLikeString__BackingField = value;
		}
	}

	public string Size
	{
		get
		{
			return Size__BackingField;
		}
		set
		{
			Size__BackingField = value;
		}
	}
}
