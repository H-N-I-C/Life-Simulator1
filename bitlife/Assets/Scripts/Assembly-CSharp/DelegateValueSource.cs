using System;

public class DelegateValueSource<T> : ValueSource<T>
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<T> _003C_003E9__5_0;

		public static Action<T> _003C_003E9__5_1;

		public static Action<T> _003C_003E9__6_0;

		internal T _003C_002Ector_003Eb__5_0()
		{
			return default;
		}

		internal void _003C_002Ector_003Eb__5_1(T _)
		{
		}

		internal void _003C_002Ector_003Eb__6_0(T _)
		{
		}
	}

	private readonly Func<T> _get;

	private readonly Action<T> _set;

	public override T Value
	{
		get
		{
			return default;
		}
		set
		{
		}
	}

	public DelegateValueSource(Func<T> getter, Action<T> setter)
	{
	}

	public DelegateValueSource(Func<T> getter)
	{
	}
}
