using Genesis.CSV;
using Genesis.Interfaces;
using UnityEngine;

public class ScriptableCSVDataSource : AbstractScriptableDataSource
{
	private TextAsset _CSVResource;

	private string _ExteriorTitleKey;

	private string _TitleKey;

	private string _BodyKey;

	private bool m_FeedbackStyleScenario;

	private string[] _ScenarioChoiceKeys;

	private CSVDocument _CSVDocument;

	private CSVLine _RandomLine;

	private void OnEnable()
	{
	}

	private void RandomizeNextLine()
	{
	}

	public override bool AreChoicesRandomized()
	{
		return false;
	}

	public override bool IsFeedbackScenario()
	{
		return false;
	}

	public override string GenerateExteriorTitle()
	{
		return null;
	}

	public override string GenerateTitle()
	{
		return null;
	}

	public override string GenerateBody()
	{
		return null;
	}

	public override ScenarioChoice[] GenerateScenarioChoices()
	{
		return null;
	}

	public override IScenarioDataSource GetNextDataSourceForUserSelection(IScenarioUserSelection userSelection)
	{
		return null;
	}
}
