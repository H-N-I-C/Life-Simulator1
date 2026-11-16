using System;

[Serializable]
public class SimFightClubBuff
{
	public FIGHT_CLUB_BUFF_TYPE Type;

	public SimFightClubBuff(FIGHT_CLUB_BUFF_TYPE type)
	{
	}

	public string GetAdjective()
	{
		return null;
	}

	public float GetAmount()
	{
		return 0f;
	}

	public static string GetAdjectiveForBuffTYpe(FIGHT_CLUB_BUFF_TYPE type)
	{
		return null;
	}

	public static float GetBuffAmountForType(FIGHT_CLUB_BUFF_TYPE type)
	{
		return 0f;
	}

	public static string GetBuffAttribute(FIGHT_CLUB_BUFF_TYPE type)
	{
		return null;
	}
}
