public class TranslationApiProfile
{
	private TranslationApiType Type__BackingField;

	private int MinVersionForLocalLLM__BackingField;

	private int AndroidMinCpuCount__BackingField;

	private int AndroidMinRamAmount__BackingField;

	private bool Enabled__BackingField;

	private int Rollout__BackingField;

	public TranslationApiType Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public int MinVersionForLocalLLM
	{
		get
		{
			return MinVersionForLocalLLM__BackingField;
		}
		set
		{
			MinVersionForLocalLLM__BackingField = value;
		}
	}

	public int AndroidMinCpuCount
	{
		get
		{
			return AndroidMinCpuCount__BackingField;
		}
		set
		{
			AndroidMinCpuCount__BackingField = value;
		}
	}

	public int AndroidMinRamAmount
	{
		get
		{
			return AndroidMinRamAmount__BackingField;
		}
		set
		{
			AndroidMinRamAmount__BackingField = value;
		}
	}

	public bool Enabled
	{
		get
		{
			return Enabled__BackingField;
		}
		set
		{
			Enabled__BackingField = value;
		}
	}

	public int Rollout
	{
		get
		{
			return Rollout__BackingField;
		}
		set
		{
			Rollout__BackingField = value;
		}
	}
}
