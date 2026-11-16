public class SimBurgleItem
{
	private int Type__BackingField;

	private float ItemValue__BackingField;

	public int Type
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

	public float ItemValue
	{
		get
		{
			return ItemValue__BackingField;
		}
		set
		{
			ItemValue__BackingField = value;
		}
	}

	public SimBurgleItem(int theType, DataStreetPerson homeOwner)
	{
	}

	public string DescriptiveName()
	{
		return null;
	}

	public string EmojiString()
	{
		return null;
	}

	public static int GetValueForItemTypeForMoneyScore(int theType, int moneyScore)
	{
		return 0;
	}
}
