public class DataMovie
{
	private string Title__BackingField;

	private int Genre__BackingField;

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		set
		{
			Title__BackingField = value;
		}
	}

	public int Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public string GetTitleForPlace(SimPlace place)
	{
		return null;
	}

	public string GenreString()
	{
		return null;
	}
}
