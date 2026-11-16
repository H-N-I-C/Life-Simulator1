public class DataMafiaNickname
{
	private string Name__BackingField;

	private string MobCountryCode__BackingField;

	private int Gender__BackingField;

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

	public string MobCountryCode
	{
		get
		{
			return MobCountryCode__BackingField;
		}
		set
		{
			MobCountryCode__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public string GetFullNicknameForPersonName(SimPersonName personName)
	{
		return null;
	}
}
