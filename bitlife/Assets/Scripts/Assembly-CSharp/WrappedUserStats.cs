using System.Collections;
using Game;

public class WrappedUserStats
{
	private int achievements__BackingField;

	private int babies__BackingField;

	private int cops_called__BackingField;

	private int drained__BackingField;

	private int famous__BackingField;

	private int generation__BackingField;

	private int heirlooms__BackingField;

	private int lovers__BackingField;

	private int murders__BackingField;

	private long net_worth__BackingField;

	private int purchases__BackingField;

	private string hasVampire__BackingField;

	private readonly GameManager _gameManager;

	private readonly GraveyardManager _graveyardManager;

	public int achievements
	{
		get
		{
			return achievements__BackingField;
		}
		set
		{
			achievements__BackingField = value;
		}
	}

	public int babies
	{
		get
		{
			return babies__BackingField;
		}
		set
		{
			babies__BackingField = value;
		}
	}

	public int cops_called
	{
		get
		{
			return cops_called__BackingField;
		}
		set
		{
			cops_called__BackingField = value;
		}
	}

	public int drained
	{
		get
		{
			return drained__BackingField;
		}
		set
		{
			drained__BackingField = value;
		}
	}

	public int famous
	{
		get
		{
			return famous__BackingField;
		}
		set
		{
			famous__BackingField = value;
		}
	}

	public int generation
	{
		get
		{
			return generation__BackingField;
		}
		set
		{
			generation__BackingField = value;
		}
	}

	public int heirlooms
	{
		get
		{
			return heirlooms__BackingField;
		}
		set
		{
			heirlooms__BackingField = value;
		}
	}

	public int lovers
	{
		get
		{
			return lovers__BackingField;
		}
		set
		{
			lovers__BackingField = value;
		}
	}

	public int murders
	{
		get
		{
			return murders__BackingField;
		}
		set
		{
			murders__BackingField = value;
		}
	}

	public long net_worth
	{
		get
		{
			return net_worth__BackingField;
		}
		set
		{
			net_worth__BackingField = value;
		}
	}

	public int purchases
	{
		get
		{
			return purchases__BackingField;
		}
		set
		{
			purchases__BackingField = value;
		}
	}

	public string hasVampire
	{
		get
		{
			return hasVampire__BackingField;
		}
		set
		{
			hasVampire__BackingField = value;
		}
	}

	public WrappedUserStats()
	{
	}

	public WrappedUserStats(GameManager gameManager, GraveyardManager graveyardManager)
	{
	}

	public void SetByLife()
	{
	}

	private void SetByAccount(Life life, ArrayList graveArray, GameVars gameVars)
	{
	}

	public int CountPositiveStats()
	{
		return 0;
	}

	public string Serialize()
	{
		return null;
	}
}
