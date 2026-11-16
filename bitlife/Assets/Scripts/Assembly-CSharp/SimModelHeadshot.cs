using System;

[Serializable]
public class SimModelHeadshot
{
	private PersonEmojiScript EmojiScript__BackingField;

	private string Outfit__BackingField;

	private string Background__BackingField;

	private string Expression__BackingField;

	private bool Grayscale__BackingField;

	private int AgeAtLastTaken__BackingField;

	public PersonEmojiScript EmojiScript
	{
		get
		{
			return EmojiScript__BackingField;
		}
		set
		{
			EmojiScript__BackingField = value;
		}
	}

	public string Outfit
	{
		get
		{
			return Outfit__BackingField;
		}
		set
		{
			Outfit__BackingField = value;
		}
	}

	public string Background
	{
		get
		{
			return Background__BackingField;
		}
		set
		{
			Background__BackingField = value;
		}
	}

	public string Expression
	{
		get
		{
			return Expression__BackingField;
		}
		set
		{
			Expression__BackingField = value;
		}
	}

	public bool Grayscale
	{
		get
		{
			return Grayscale__BackingField;
		}
		set
		{
			Grayscale__BackingField = value;
		}
	}

	public int AgeAtLastTaken
	{
		get
		{
			return AgeAtLastTaken__BackingField;
		}
		set
		{
			AgeAtLastTaken__BackingField = value;
		}
	}

	public static bool GetHeadshotExists()
	{
		return false;
	}
}
