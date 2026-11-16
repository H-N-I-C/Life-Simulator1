using System;

public class LoginReward
{
	private DateTime NextRewardDate__BackingField;

	public DateTime NextRewardDate
	{
		get
		{
			return NextRewardDate__BackingField;
		}
		set
		{
			NextRewardDate__BackingField = value;
		}
	}

	public string Description()
	{
		return null;
	}
}
