using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioHustleStreetNewFeedback : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<int> _003C_003E9__5_0;

		internal void _003CDelayAlert_003Eb__5_0(int index)
		{
		}
	}

	private sealed class _003CDelayAlert_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ScenarioHustleStreetNewFeedback _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDelayAlert_003Ed__5(int _003C_003E1__state)
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

	protected string _narrative;

	protected SimHustleStreet _hustleStreet;

	public ScenarioHustleStreetNewFeedback(SimHustleStreet theStreet)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public IEnumerator DelayAlert()
	{
		return null;
	}
}
