using System;

[Serializable]
public class SimCasinoMafiaMember : SimCasinoGambler
{
	private string Nickname__BackingField;

	public string Nickname
	{
		get
		{
			return Nickname__BackingField;
		}
		set
		{
			Nickname__BackingField = value;
		}
	}

	public SimCasinoMafiaMember(bool initHere, int gender)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override string GamblerName()
	{
		return null;
	}

	public override string OccupationString()
	{
		return null;
	}

	public void SetupNickname()
	{
	}
}
