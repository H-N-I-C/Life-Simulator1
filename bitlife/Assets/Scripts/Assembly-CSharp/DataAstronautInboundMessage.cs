using System.Collections;

public class DataAstronautInboundMessage
{
	private string Message__BackingField;

	private ArrayList IncorrectAnswers__BackingField;

	public string Message
	{
		get
		{
			return Message__BackingField;
		}
		set
		{
			Message__BackingField = value;
		}
	}

	public ArrayList IncorrectAnswers
	{
		get
		{
			return IncorrectAnswers__BackingField;
		}
		set
		{
			IncorrectAnswers__BackingField = value;
		}
	}
}
