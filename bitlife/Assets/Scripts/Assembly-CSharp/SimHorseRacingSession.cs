using System.Collections;

public class SimHorseRacingSession
{
	private ArrayList _HorseArray__BackingField;

	private string BetString__BackingField;

	private int BetAmount__BackingField;

	private string SelectedHorse__BackingField;

	private string WinningHorse__BackingField;

	private string FallOverHorse__BackingField;

	private ArrayList _HorseArray
	{
		get
		{
			return _HorseArray__BackingField;
		}
		set
		{
			_HorseArray__BackingField = value;
		}
	}

	public ArrayList HorseArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string BetString
	{
		get
		{
			return BetString__BackingField;
		}
		set
		{
			BetString__BackingField = value;
		}
	}

	public int BetAmount
	{
		get
		{
			return BetAmount__BackingField;
		}
		set
		{
			BetAmount__BackingField = value;
		}
	}

	public string SelectedHorse
	{
		get
		{
			return SelectedHorse__BackingField;
		}
		set
		{
			SelectedHorse__BackingField = value;
		}
	}

	public string WinningHorse
	{
		get
		{
			return WinningHorse__BackingField;
		}
		set
		{
			WinningHorse__BackingField = value;
		}
	}

	public string FallOverHorse
	{
		get
		{
			return FallOverHorse__BackingField;
		}
		set
		{
			FallOverHorse__BackingField = value;
		}
	}

	public bool HeroWins()
	{
		return false;
	}
}
