using System;
using System.Collections;
using System.Collections.Generic;

public class BirthdayScavengerHunt : ScavengerHunt
{
	private sealed class _003CGetEnumerator_003Ed__12 : IEnumerator<ScavengerItem>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private ScavengerItem _003C_003E2__current;

		ScavengerItem IEnumerator<ScavengerItem>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetEnumerator_003Ed__12(int _003C_003E1__state)
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

	public static readonly ScavengerItem SevenBy7;

	public static readonly ScavengerItem Crime14;

	public static readonly ScavengerItem Clubbing21;

	public static readonly ScavengerItem Haunted28;

	public static readonly ScavengerItem Casino35;

	public static readonly ScavengerItem Plastic42;

	public static readonly ScavengerItem PetKevin49;

	public static readonly ScavengerItem HookupCoworker56;

	public static readonly ScavengerItem CatNas63;

	public static readonly ScavengerItem Blood70;

	public static readonly ScavengerItem Prison77;

	public BirthdayScavengerHunt()
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

	public override string GetRedeemCodeByCollectible(HolidayCollectibleMetadata collectible)
	{
		return null;
	}
}
