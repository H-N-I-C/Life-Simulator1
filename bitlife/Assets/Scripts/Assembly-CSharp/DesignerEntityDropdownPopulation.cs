using System;
using System.Collections.Generic;

[Serializable]
public class DesignerEntityDropdownPopulation
{
	private string DataSource__BackingField;

	private string GenderFilter__BackingField;

	private string TitleProperty__BackingField;

	private string SubtitleProperty__BackingField;

	private string DisplayTemplate__BackingField;

	private string SortTilesByMetric__BackingField;

	private bool SortTilesIsAscending__BackingField;

	private List<string> ExcludedEntityIds__BackingField;

	public string DataSource
	{
		get
		{
			return DataSource__BackingField;
		}
		set
		{
			DataSource__BackingField = value;
		}
	}

	public string GenderFilter
	{
		get
		{
			return GenderFilter__BackingField;
		}
		set
		{
			GenderFilter__BackingField = value;
		}
	}

	public string TitleProperty
	{
		get
		{
			return TitleProperty__BackingField;
		}
		set
		{
			TitleProperty__BackingField = value;
		}
	}

	public string SubtitleProperty
	{
		get
		{
			return SubtitleProperty__BackingField;
		}
		set
		{
			SubtitleProperty__BackingField = value;
		}
	}

	public string DisplayTemplate
	{
		get
		{
			return DisplayTemplate__BackingField;
		}
		set
		{
			DisplayTemplate__BackingField = value;
		}
	}

	public string SortTilesByMetric
	{
		get
		{
			return SortTilesByMetric__BackingField;
		}
		set
		{
			SortTilesByMetric__BackingField = value;
		}
	}

	public bool SortTilesIsAscending
	{
		get
		{
			return SortTilesIsAscending__BackingField;
		}
		set
		{
			SortTilesIsAscending__BackingField = value;
		}
	}

	public List<string> ExcludedEntityIds
	{
		get
		{
			return ExcludedEntityIds__BackingField;
		}
		set
		{
			ExcludedEntityIds__BackingField = value;
		}
	}
}
