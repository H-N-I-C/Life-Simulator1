using System.Collections;

public class ScenarioTranslationConfig : Scenario
{
	private readonly ArrayList _languages;

	private readonly ArrayList _systems;

	private readonly int _selectedLanguage;

	private readonly int _selectedSystem;

	private readonly bool _showSystems;

	private readonly bool _isFtue;

	private readonly PreferredLanguageConfirmationTelemetry _confirmationTelemetry;

	public static bool IsFinishedSelection;

	public ScenarioTranslationConfig(ArrayList languages, ArrayList systems, int selectedLanguage, int selectedSystem, bool showSystems = true, bool isFtue = false, bool trackPreferredLanguageConfirmation = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
