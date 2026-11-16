using System.Collections.Generic;

public class TranslationInitModel
{
	private sealed class _003C_003Ec__DisplayClass48_0
	{
		public string country;

		internal bool _003CIsTranslationSystemAvailable_003Eb__0(string t)
		{
			return false;
		}
	}

	private string GeminiApiKey__BackingField;

	private string BedRockApiKey__BackingField;

	private string GeminiUrl__BackingField;

	private string GeminiModel__BackingField;

	private string BedRockUrl__BackingField;

	private List<string> LanguagesAvailable__BackingField;

	private List<string> CountriesAvailable__BackingField;

	private List<TranslationApiProfile> IOSProfiles__BackingField;

	private List<TranslationApiProfile> AndroidProfiles__BackingField;

	private List<TranslationApiProfile> EditorProfiles__BackingField;

	private int RetryCountMax__BackingField;

	private int RetrySeconds__BackingField;

	public string GeminiApiKey
	{
		get
		{
			return GeminiApiKey__BackingField;
		}
		set
		{
			GeminiApiKey__BackingField = value;
		}
	}

	public string BedRockApiKey
	{
		get
		{
			return BedRockApiKey__BackingField;
		}
		set
		{
			BedRockApiKey__BackingField = value;
		}
	}

	public string GeminiUrl
	{
		get
		{
			return GeminiUrl__BackingField;
		}
		set
		{
			GeminiUrl__BackingField = value;
		}
	}

	public string GeminiModel
	{
		get
		{
			return GeminiModel__BackingField;
		}
		set
		{
			GeminiModel__BackingField = value;
		}
	}

	public string BedRockUrl
	{
		get
		{
			return BedRockUrl__BackingField;
		}
		set
		{
			BedRockUrl__BackingField = value;
		}
	}

	public List<string> LanguagesAvailable
	{
		get
		{
			return LanguagesAvailable__BackingField;
		}
		set
		{
			LanguagesAvailable__BackingField = value;
		}
	}

	public List<string> CountriesAvailable
	{
		get
		{
			return CountriesAvailable__BackingField;
		}
		set
		{
			CountriesAvailable__BackingField = value;
		}
	}

	public List<TranslationApiProfile> IOSProfiles
	{
		get
		{
			return IOSProfiles__BackingField;
		}
		set
		{
			IOSProfiles__BackingField = value;
		}
	}

	public List<TranslationApiProfile> AndroidProfiles
	{
		get
		{
			return AndroidProfiles__BackingField;
		}
		set
		{
			AndroidProfiles__BackingField = value;
		}
	}

	public List<TranslationApiProfile> EditorProfiles
	{
		get
		{
			return EditorProfiles__BackingField;
		}
		set
		{
			EditorProfiles__BackingField = value;
		}
	}

	public int RetryCountMax
	{
		get
		{
			return RetryCountMax__BackingField;
		}
		set
		{
			RetryCountMax__BackingField = value;
		}
	}

	public int RetrySeconds
	{
		get
		{
			return RetrySeconds__BackingField;
		}
		set
		{
			RetrySeconds__BackingField = value;
		}
	}

	public bool IsTranslationSystemAvailable(string country)
	{
		return false;
	}

	public bool IsRolloutActive(int profileRollout)
	{
		return false;
	}
}
