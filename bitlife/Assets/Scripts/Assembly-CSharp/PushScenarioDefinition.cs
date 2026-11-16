using System;
using System.Reflection;

public class PushScenarioDefinition
{
	private readonly Type ScenarioType__BackingField;

	private readonly PushScenarioAttribute ScenarioMetadata__BackingField;

	private readonly MethodInfo ValidationMethod__BackingField;

	private readonly PushScenarioValidationAttribute ScenarioValidation__BackingField;

	private readonly MethodInfo ConstructorMethod__BackingField;

	private readonly ConstructorInfo DefaultConstructor__BackingField;

	private readonly PushScenarioConstructorAttribute ScenarioConstructor__BackingField;

	public Type ScenarioType => ScenarioType__BackingField;

	public PushScenarioAttribute ScenarioMetadata => ScenarioMetadata__BackingField;

	public MethodInfo ValidationMethod => ValidationMethod__BackingField;

	public PushScenarioValidationAttribute ScenarioValidation => ScenarioValidation__BackingField;

	public MethodInfo ConstructorMethod => ConstructorMethod__BackingField;

	public ConstructorInfo DefaultConstructor => DefaultConstructor__BackingField;

	public PushScenarioConstructorAttribute ScenarioConstructor => ScenarioConstructor__BackingField;

	public string Category => null;

	public string SubCategory => null;

	public string ID => null;

	private PushScenarioDefinition(Type scenarioType)
	{
	}

	public bool Validate()
	{
		return false;
	}

	public bool LocalValidation()
	{
		return false;
	}

	public Scenario Construct()
	{
		return null;
	}
}
