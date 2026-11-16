using System.Collections;

public class DataAstronautTrainingQuestion
{
	private int CommonnessScore__BackingField;

	private string Question__BackingField;

	private ArrayList CorrectAnswers__BackingField;

	private ArrayList DummyAnswers__BackingField;

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
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

	public ArrayList CorrectAnswers
	{
		get
		{
			return CorrectAnswers__BackingField;
		}
		set
		{
			CorrectAnswers__BackingField = value;
		}
	}

	public ArrayList DummyAnswers
	{
		get
		{
			return DummyAnswers__BackingField;
		}
		set
		{
			DummyAnswers__BackingField = value;
		}
	}
}
