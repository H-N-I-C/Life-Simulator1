using System;

public sealed class DeferredSelectionList
{
	private readonly Func<List> listBuilder;

	public DeferredSelectionList(Func<List> listBuilder)
	{
	}

	public bool TryBuild(out List list)
	{
		list = null;
		return false;
	}
}
