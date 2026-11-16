using System.Collections;

public class DataSchoolFratQuestion
{
	private string Question__BackingField;

	private ArrayList AnswerArray__BackingField;

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

	public ArrayList AnswerArray
	{
		get
		{
			return AnswerArray__BackingField;
		}
		set
		{
			AnswerArray__BackingField = value;
		}
	}
}
