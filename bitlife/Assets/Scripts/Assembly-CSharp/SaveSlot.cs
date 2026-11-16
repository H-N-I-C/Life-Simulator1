using System;

[Serializable]
public class SaveSlot : IComparable
{
	public int IndexZI;

	public string HeroName;

	public string LastPlayed;

	public bool IsLive;

	public int Age;

	public string CustomLifeId;

	public PersonEmojiScript EmojiScript;

	public bool IsCurrentSlot()
	{
		return false;
	}

	public string EmojiString()
	{
		return null;
	}

	public int CompareTo(object anotherSlot)
	{
		return 0;
	}
}
