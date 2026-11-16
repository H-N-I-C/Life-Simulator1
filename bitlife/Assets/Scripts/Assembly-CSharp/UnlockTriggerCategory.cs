using System;
using System.Collections;
using System.Collections.Generic;

public abstract class UnlockTriggerCategory<T> : UnlockTrigger<T>, IReadOnlyCollection<IUnlockTriggerWithContext<T>>, IEnumerable<IUnlockTriggerWithContext<T>>, IEnumerable where T : struct
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<IUnlockTriggerWithContext<T>, bool> _003C_003E9__8_0;

		internal bool _003CIsValid_003Eb__8_0(IUnlockTriggerWithContext<T> x)
		{
			return false;
		}
	}

	private sealed class _003CGetEnumerator_003Ed__7 : IEnumerator<IUnlockTriggerWithContext<T>>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private IUnlockTriggerWithContext<T> _003C_003E2__current;

		public UnlockTriggerCategory<T> _003C_003E4__this;

		private IUnlockTriggerWithContext<T>[] _003C_003E7__wrap1;

		private int _003C_003E7__wrap2;

		IUnlockTriggerWithContext<T> IEnumerator<IUnlockTriggerWithContext<T>>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetEnumerator_003Ed__7(int _003C_003E1__state)
		{
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
		}
	}

	private T ActiveContext__BackingField;

	protected T ActiveContext
	{
		get
		{
			return ActiveContext__BackingField;
		}
		private set
		{
			ActiveContext__BackingField = value;
		}
	}

	public int Count => 0;

	IEnumerator IEnumerable.GetEnumerator()
	{
		return null;
	}

	public IEnumerator<IUnlockTriggerWithContext<T>> GetEnumerator()
	{
		return null;
	}

	public override bool IsValid()
	{
		return false;
	}

	protected override bool MeetsUnlockConditions(T _)
	{
		return false;
	}

	protected sealed override Scenario UnlockContent(T context)
	{
		return null;
	}

	protected abstract IUnlockTriggerWithContext<T>[] GetAllContentUnlocked();
}
