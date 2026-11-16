using Genesis.Interfaces;
using UnityEngine;

public abstract class AbstractScriptableDataSource : ScriptableObject, IScenarioDataSource
{
	public abstract bool AreChoicesRandomized();

	public abstract bool IsFeedbackScenario();

	public abstract string GenerateExteriorTitle();

	public abstract string GenerateTitle();

	public abstract string GenerateBody();

	public abstract ScenarioChoice[] GenerateScenarioChoices();

	public abstract IScenarioDataSource GetNextDataSourceForUserSelection(IScenarioUserSelection userSelection);
}
