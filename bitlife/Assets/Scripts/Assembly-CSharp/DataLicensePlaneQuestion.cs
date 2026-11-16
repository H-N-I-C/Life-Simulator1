using System.Collections;

public class DataLicensePlaneQuestion
{
	private int IndexNI__BackingField;

	private string Question__BackingField;

	private string CorrectAnswerString__BackingField;

	private ArrayList IncorrectAnswerArray__BackingField;

	public int IndexNI
	{
		get
		{
			return IndexNI__BackingField;
		}
		set
		{
			IndexNI__BackingField = value;
		}
	}

	public string Question
	{
		get
		{
			return Question__BackingField;
		}
		set
		{
			Question__BackingField = value;
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
