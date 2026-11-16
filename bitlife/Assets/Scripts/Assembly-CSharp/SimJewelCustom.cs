using System;

[Serializable]
public class SimJewelCustom : SimJewel
{
	private readonly string _shortDescription;

	private readonly string _emoji;

	public SimJewelCustom(float minValue, float maxValue, bool willBeFake, bool heroKnowsAuthenticity, JEWELRY_TYPE jewelryType, string gemstoneName, string metalName, string shortDescription, string emoji)
		: base(0f, 0f, isWatch: false)
	{
	}

	public override string ShortDescription()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
