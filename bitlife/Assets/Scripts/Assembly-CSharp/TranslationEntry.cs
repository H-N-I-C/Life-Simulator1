public class TranslationEntry
{
	private string Original__BackingField;

	private string Translation__BackingField;

	private bool IsProviderTranslation__BackingField;

	public string Original
	{
		get
		{
			return Original__BackingField;
		}
		set
		{
			Original__BackingField = value;
		}
	}

	public string Translation
	{
		get
		{
			return Translation__BackingField;
		}
		set
		{
			Translation__BackingField = value;
		}
	}

	public bool IsProviderTranslation
	{
		get
		{
			return IsProviderTranslation__BackingField;
		}
		set
		{
			IsProviderTranslation__BackingField = value;
		}
	}
}
