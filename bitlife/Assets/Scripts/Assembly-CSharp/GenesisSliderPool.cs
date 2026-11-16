using System.Collections.Generic;
using BitLife.UI.CandyObjects;

public class GenesisSliderPool
{
	private class Entry
	{
		public CandySlider Candy;

		public ScenarioSlider Data;
	}

	private const double Epsilon = 0.0001;

	private readonly List<Entry> _entries;

	private readonly double _total;

	private bool _balancing;

	public int Count => 0;

	public GenesisSliderPool(double poolTotal)
	{
	}

	public void Add(CandySlider candy, ScenarioSlider data)
	{
	}

	public void Initialize()
	{
	}

	private void NormalizeToTotal()
	{
	}

	private void OnSliderChanged(CandySlider changed)
	{
	}

	private Entry FindEntry(CandySlider candy)
	{
		return null;
	}

	private void ApplyValue(Entry e, double raw)
	{
	}
}
