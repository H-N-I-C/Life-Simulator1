public class DataAstronautNebulaShape
{
	private string Shape__BackingField;

	private bool UsesDefiniteArticle__BackingField;

	private bool UsesIndefiniteArticle__BackingField;

	public string Shape
	{
		get
		{
			return Shape__BackingField;
		}
		set
		{
			Shape__BackingField = value;
		}
	}

	public bool UsesDefiniteArticle
	{
		get
		{
			return UsesDefiniteArticle__BackingField;
		}
		set
		{
			UsesDefiniteArticle__BackingField = value;
		}
	}

	public bool UsesIndefiniteArticle
	{
		get
		{
			return UsesIndefiniteArticle__BackingField;
		}
		set
		{
			UsesIndefiniteArticle__BackingField = value;
		}
	}
}
