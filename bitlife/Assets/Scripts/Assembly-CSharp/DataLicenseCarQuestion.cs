using System.Collections;

public class DataLicenseCarQuestion
{
	private int Sign__BackingField;

	private string CorrectAnswerString__BackingField;

	private ArrayList IncorrectAnswerArray__BackingField;

	public int Sign
	{
		get
		{
			return Sign__BackingField;
		}
		set
		{
			Sign__BackingField = value;
		}
	}

	public string CorrectAnswerString
	{
		get
		{
			return CorrectAnswerString__BackingField;
		}
		set
		{
			CorrectAnswerString__BackingField = value;
		}
	}

	public ArrayList IncorrectAnswerArray
	{
		get
		{
			return IncorrectAnswerArray__BackingField;
		}
		set
		{
			IncorrectAnswerArray__BackingField = value;
		}
	}
}
