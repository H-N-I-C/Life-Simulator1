using System;
using System.Collections.Generic;

public abstract class SROptionProvider
{
	private readonly List<SROptionEntry> _entries;

	private bool _built;

	private Action<SROptionEntry> m_OptionAdded;

	private Action<SROptionEntry> m_OptionRemoved;

	public event Action<SROptionEntry> OptionAdded
	{
		add
		{
		}
		remove
		{
		}
	}

	public event Action<SROptionEntry> OptionRemoved
	{
		add
		{
		}
		remove
		{
		}
	}

	public IReadOnlyList<SROptionEntry> GetOptions()
	{
		return null;
	}

	protected abstract void Build();

	protected void AddOption(SROptionEntry entry)
	{
	}

	protected bool RemoveOption(SROptionEntry entry)
	{
		return false;
	}

	private void EnsureBuilt()
	{
	}
}
