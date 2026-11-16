using System;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameDealerDrugCutProduction : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	public int NumberOfBaseDosesRequired;

	public int NumberOfCutDosesCreated;

	public int MaxCutDoses;

	private bool _initialized;

	private float _width;

	private float _listStartVertical;

	private Scenario _scenario;

	private MiniGameDataDealerDrugCutProduction _data;

	private GameObject _baseDosesRequiredLabelObject;

	private GameObject _baseDosesRequiredCountLabelObject;

	private MiniGameDealerDrugCutProductionSlider _slider;

	private bool _buttonJustPressed;

	private ScenarioMinigameTextRegistry _textRegistry;

	private bool _gameOverProcessed;

	private const string SLIDER_PREFAB_PATH = "prefabs/MiniGames/DealerDrugCutProduction/DealerDrugCutProductionSlider";

	private static readonly string[] RequiredChildren;

	private static readonly string[] RequiredTextKeys;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float pWidth, Scenario pScenario)
	{
	}

	private void BuildLayout()
	{
	}

	public void ButtonUp()
	{
	}

	public void ButtonDown()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private bool ButtonUpVerification()
	{
		return false;
	}

	public void GameOver()
	{
	}

	public void ExitGame()
	{
	}

	public void UpdateTables()
	{
	}

	private Rect RectTransformToWorldSpace(RectTransform transform)
	{
		return default;
	}

	private string FindMissingDependency()
	{
		return null;
	}

	private void ReportFailure(Exception e, string stage)
	{
	}

	private Dictionary<string, string> BuildDiagnosticContext(string stage)
	{
		return null;
	}

	private string SafeDosesAvailable()
	{
		return null;
	}

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void SetWidth(float width)
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public void SetHeightConstraint()
	{
	}
}
