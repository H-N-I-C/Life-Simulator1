using System;

[Serializable]
public class SimVampireHonorific
{
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public SimPerson person;

		internal string _003CGetHonorificString_003Eb__0(SimVampireHonorificPart x)
		{
			return null;
		}
	}

	private SimVampireHonorificPart[] HonorificParts__BackingField;

	private string HonorificFormat__BackingField;

	public SimVampireHonorificPart[] HonorificParts
	{
		get
		{
			return HonorificParts__BackingField;
		}
		set
		{
			HonorificParts__BackingField = value;
		}
	}

	public string HonorificFormat
	{
		get
		{
			return HonorificFormat__BackingField;
		}
		set
		{
			HonorificFormat__BackingField = value;
		}
	}

	public string GetHonorificString(SimPerson person)
	{
		return null;
	}
}
