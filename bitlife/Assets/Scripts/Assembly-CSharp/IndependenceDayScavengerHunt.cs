using System;
using System.Collections;
using System.Collections.Generic;

public class IndependenceDayScavengerHunt : ScavengerHunt
{
	private sealed class _003CGetEnumerator_003Ed__11 : IEnumerator<ScavengerItem>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private ScavengerItem _003C_003E2__current;

		ScavengerItem IEnumerator<ScavengerItem>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetEnumerator_003Ed__11(int _003C_003E1__state)
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

	public static readonly ScavengerItem TeaBag;

	public static readonly ScavengerItem Soldier;

	public static readonly ScavengerItem Football;

	public static readonly ScavengerItem ModelT;

	public static readonly ScavengerItem Burger;

	public static readonly ScavengerItem Star;

	public static readonly ScavengerItem Sun;

	public static readonly ScavengerItem Dream;

	public static readonly ScavengerItem Derby;

	public static readonly ScavengerItem Bike;

	public IndependenceDayScavengerHunt()
		: base(HOLIDAY_COLLECTIBLE_TYPE.NONE)
	{
	}

	public override IEnumerator<ScavengerItem> GetEnumerator()
	{
		return null;
	}

	public override DataAccessory GetCompletionAccessory()
	{
		return null;
	}

	public override DateTime GetStartDate()
	{
		return default;
	}

	public override DateTime GetEndDate()
	{
		return default;
	}
}
