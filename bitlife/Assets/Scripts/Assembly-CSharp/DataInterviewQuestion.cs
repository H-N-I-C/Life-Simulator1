using System.Collections;

public class DataInterviewQuestion
{
	private string Question__BackingField;

	private string Cta__BackingField;

	private int CommonnessScore__BackingField;

	private DataInterviewAnswer DefaultAnswer__BackingField;

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

	public string Cta
	{
		get
		{
			return Cta__BackingField;
		}
		set
		{
			Cta__BackingField = value;
		}
	}

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

	public DataInterviewAnswer DefaultAnswer
	{
		get
		{
			return DefaultAnswer__BackingField;
		}
		set
		{
			DefaultAnswer__BackingField = value;
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
