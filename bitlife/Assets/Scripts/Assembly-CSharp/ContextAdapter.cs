using System;
using System.Collections;
using System.Collections.Generic;

public class ContextAdapter<inType, outType> : UnlockTrigger<outType>, IEnumerable<IUnlockTriggerWithContext<inType>>, IEnumerable where outType : struct
{
	private sealed class _003CGetEnumerator_003Ed__13 : IEnumerator<IUnlockTriggerWithContext<inType>>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private IUnlockTriggerWithContext<inType> _003C_003E2__current;

		public ContextAdapter<inType, outType> _003C_003E4__this;

		IUnlockTriggerWithContext<inType> IEnumerator<IUnlockTriggerWithContext<inType>>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetEnumerator_003Ed__13(int _003C_003E1__state)
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

	private readonly IUnlockTriggerWithContext<inType> _anyTypeInContextTrigger;

	private readonly Func<outType, bool> _runCondition;

	private readonly Func<outType, inType> _contextConversion;

	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	public ContextAdapter(IUnlockTriggerWithContext<inType> anyTypeIn)
	{
	}

	public ContextAdapter(IUnlockTriggerWithContext<inType> anyTypeIn, Func<outType, inType> contextConversion)
	{
	}

	public ContextAdapter(IUnlockTriggerWithContext<inType> anyTypeIn, Func<outType, bool> runCondition)
	{
	}

	public ContextAdapter(IUnlockTriggerWithContext<inType> anyTypeIn, Func<outType, inType> contextConversion, Func<outType, bool> runCondition)
	{
	}

	public override bool IsValid()
	{
		return false;
	}

	protected override Scenario UnlockContent(outType context)
	{
		return null;
	}

	protected override bool MeetsUnlockConditions(outType context)
	{
		return false;
	}

	public IEnumerator<IUnlockTriggerWithContext<inType>> GetEnumerator()
	{
		return null;
	}

	public override string ToString()
	{
		return null;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return null;
	}

	private inType ConvertContextConversion(outType contextToConvert)
	{
		return default;
	}

	private static bool TrueCondition(outType _)
	{
		return false;
	}

	private static inType DefaultContextConversion(outType outType)
	{
		return default;
	}
}
