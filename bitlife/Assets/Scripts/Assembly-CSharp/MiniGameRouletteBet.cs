using System.Collections.Generic;
using BitLife.UI.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameRouletteBet : MonoBehaviour, IguiObject
{
	private RouletteBetItem _rouletteBetItemPrefab;

	private Button _betButton;

	private RectTransform _rect;

	private GridLayoutGroup _numbersLayoutGroup;

	private VerticalLayoutGroup _colorsLayoutGroup;

	private VerticalLayoutGroup _zeroLayoutGroup;

	private TextMeshProUGUI _spinText;

	private SimCasinoSession _session;

	private ScenarioRouletteCasinoBet _scenario;

	private const int _numberItemRows = 4;

	private const int _numberItemCols = 3;

	private const float _itemSize = 220f;

	private const float _itemGapSize = 0f;

	private const float _itemBorderSize = 40f;

	private const float _itemMarginSize = 60f;

	private const float _numberItemsWidth = 660f;

	private const float _numberItemsHeight = 880f;

	private const float _colorItemsWidth = 220f;

	private const float _colorItemsHeight = 440f;

	private const float _totalWidth = 940f;

	private const float _totalHeight = 1050f;

	private List<RouletteBetItem> _betItems;

	private bool _initialBetItemSelected;

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	private void InitWithParameters(float width, Scenario scenario)
	{
	}

	public void SetHeightConstraint()
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	private void InitItems()
	{
	}

	private void InitItem(Transform parent, int number = -1, ROULETTE_COLOR color = ROULETTE_COLOR.NONE)
	{
	}

	private void OnItemClicked(RouletteBetItem clickedItem)
	{
	}

	private void ExitGame()
	{
	}

	private RouletteBetItem GetItemForCurrentBet()
	{
		return null;
	}

	private bool _003CGetItemForCurrentBet_003Eb__32_0(RouletteBetItem i)
	{
		return false;
	}
}
