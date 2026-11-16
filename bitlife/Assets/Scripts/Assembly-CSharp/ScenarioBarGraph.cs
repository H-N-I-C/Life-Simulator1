using UnityEngine;

public class ScenarioBarGraph
{
	public string title;

	public string TranslatedTitle;

	public float value;

	public int colorScheme;

	public int boostAttribute;

	private BarGraphSourceType SourceType__BackingField;

	public BarGraphSourceType SourceType
	{
		get
		{
			return SourceType__BackingField;
		}
		private set
		{
			SourceType__BackingField = value;
		}
	}

	public ScenarioBarGraph()
	{
	}

	public static ScenarioBarGraph Relationship<T>(T target, BAR_GRAPH_COLOR_SCHEME colorScheme = BAR_GRAPH_COLOR_SCHEME.LOW_IS_RED) where T : SimPerson
	{
		return null;
	}

	public static ScenarioBarGraph Relationship(float value, BarGraphSourceType sourceType, BAR_GRAPH_COLOR_SCHEME colorScheme = BAR_GRAPH_COLOR_SCHEME.LOW_IS_RED)
	{
		return null;
	}

	public static ScenarioBarGraph Relationship(string title, float value, BarGraphSourceType sourceType, BAR_GRAPH_COLOR_SCHEME colorScheme = BAR_GRAPH_COLOR_SCHEME.LOW_IS_RED)
	{
		return null;
	}

	public ScenarioBarGraph(string title, float value, int colorScheme = 0, int boostAttribute = 0)
	{
	}

	public ScenarioBarGraph(string title, float value)
	{
	}

	public ScenarioBarGraph(string title, float value, BAR_GRAPH_COLOR_SCHEME colorScheme = BAR_GRAPH_COLOR_SCHEME.LOW_IS_RED, BOOST_ATTRIBUTE boostAttribute = BOOST_ATTRIBUTE.NONE)
	{
	}

	public Color GraphColor()
	{
		return default;
	}

	private ScenarioBarGraph Set(string title, float value, int colorScheme = 0, int boostAttribute = 0, BarGraphSourceType sourceType = BarGraphSourceType.None)
	{
		return null;
	}

	private void Validate()
	{
	}

	public string GetTitle()
	{
		return null;
	}
}
