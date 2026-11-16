using System;
using System.Collections.Generic;

public class DelegateComparer<T> : IComparer<T>
{
	private readonly Func<T, T, int> _compareAction;

	public DelegateComparer(Func<T, T, int> comparer)
	{
	}

	public int Compare(T x, T y)
	{
		return 0;
	}
}
