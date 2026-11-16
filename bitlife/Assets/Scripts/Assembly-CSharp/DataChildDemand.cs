using System.Collections;

public class DataChildDemand
{
	private int Age__BackingField;

	private int Amount__BackingField;

	private ArrayList BoyDemandArray__BackingField;

	private ArrayList GirlDemandArray__BackingField;

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public int Amount
	{
		get
		{
			return Amount__BackingField;
		}
		set
		{
			Amount__BackingField = value;
		}
	}

	public ArrayList BoyDemandArray
	{
		get
		{
			return BoyDemandArray__BackingField;
		}
		set
		{
			BoyDemandArray__BackingField = value;
		}
	}

	public ArrayList GirlDemandArray
	{
		get
		{
			return GirlDemandArray__BackingField;
		}
		set
		{
			GirlDemandArray__BackingField = value;
		}
	}
}
