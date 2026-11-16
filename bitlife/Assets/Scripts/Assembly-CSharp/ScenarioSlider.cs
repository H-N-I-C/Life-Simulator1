public class ScenarioSlider
{
	public string Title;

	public double MinAmount;

	public double MaxAmount;

	public bool UseCurrency;

	public bool HideValue;

	public float DefaultSelectedPercentage;

	public double DefaultSelectedAmount;

	public string EmojiString;

	public bool ShowCurrencyShorthand;

	public int OverrideSliderStep;

	public string PoolId;

	public double PoolTotal;

	private string TranslatedTitle__BackingField;

	public string TranslatedTitle
	{
		get
		{
			return TranslatedTitle__BackingField;
		}
		set
		{
			TranslatedTitle__BackingField = value;
		}
	}

	public ScenarioSlider()
	{
	}

	public ScenarioSlider(string title, string emojiString, float min, float max, bool useCurrency, float defaultAmount, bool showCurrencyShorthand)
	{
	}

	public string GetFormattedStringForAmount(double amount)
	{
		return null;
	}

	public string GetFormattedStringForAmount(float amount)
	{
		return null;
	}

	public int IntToRoundTo()
	{
		return 0;
	}

	public double RoundAmount(double amount)
	{
		return 0.0;
	}

	public string GetTitle()
	{
		return null;
	}
}
