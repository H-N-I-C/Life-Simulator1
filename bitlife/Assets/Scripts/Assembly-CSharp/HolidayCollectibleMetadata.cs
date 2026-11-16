using System;

[Serializable]
public class HolidayCollectibleMetadata
{
	private HOLIDAY_COLLECTIBLE_TYPE Type__BackingField;

	private string Name__BackingField;

	private string EmojiString__BackingField;

	private string Description__BackingField;

	private string DateCollected__BackingField;

	public HOLIDAY_COLLECTIBLE_TYPE Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public string EmojiString
	{
		get
		{
			return EmojiString__BackingField;
		}
		set
		{
			EmojiString__BackingField = value;
		}
	}

	public string Description
	{
		get
		{
			return Description__BackingField;
		}
		set
		{
			Description__BackingField = value;
		}
	}

	public string DateCollected
	{
		get
		{
			return DateCollected__BackingField;
		}
		set
		{
			DateCollected__BackingField = value;
		}
	}

	public HolidayCollectibleMetadata(string name, string description, HOLIDAY_COLLECTIBLE_TYPE type)
	{
	}
}
