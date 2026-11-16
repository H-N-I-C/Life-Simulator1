using System.Collections;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameAstronautLaunchProbeSettings : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private float BudgetAllocated__BackingField;

	private float TotalBudget__BackingField;

	private GameObject tableObject;

	private bool initialized;

	private float theWidth;

	private float slidersStartVertical;

	private Scenario scenario;

	private MiniGameDataAstronautLaunchProbeSettings data;

	private ArrayList sliders;

	private Vector2 tablePosition;

	public float BudgetAllocated
	{
		get
		{
			return BudgetAllocated__BackingField;
		}
		set
		{
			BudgetAllocated__BackingField = value;
		}
	}

	public float TotalBudget
	{
		get
		{
			return TotalBudget__BackingField;
		}
		set
		{
			TotalBudget__BackingField = value;
		}
	}

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

	public void InitWithParameters(float width, Scenario theScenario)
	{
	}

	public float PositionSliders()
	{
		return 0f;
	}

	public void UpdateData()
	{
	}

	private void BuildTable()
	{
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
