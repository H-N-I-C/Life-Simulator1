using System;
using System.Collections;
using System.Collections.Generic;

public class PremierLeagueScavengerHunt : ScavengerHunt
{
	private sealed class _003C_003Ec__DisplayClass11_0
	{
		public string countryCode;

		internal bool _003CGetBallTypeLocalized_003Eb__0(string location)
		{
			return false;
		}
	}

	private sealed class _003CGetEnumerator_003Ed__14 : IEnumerator<ScavengerItem>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private ScavengerItem _003C_003E2__current;

		ScavengerItem IEnumerator<ScavengerItem>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetEnumerator_003Ed__14(int _003C_003E1__state)
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

	public static readonly ScavengerItem SoccerTeam;

	public static readonly ScavengerItem Argentina;

	public static readonly ScavengerItem DonateBlood;

	public static readonly ScavengerItem MarryFamousSinger;

	public static readonly ScavengerItem PoseForMagazine;

	public static readonly ScavengerItem GoToGym;

	public static readonly ScavengerItem MoveToSpain;

	public static readonly ScavengerItem RemoteProductOnSocialMedia;

	public static readonly ScavengerItem HaveGoldenRetriever_Poker;

	public static readonly ScavengerItem Son_Cristiano;

	public static readonly ScavengerItem OwnAGulfstream;

	public static string GetBallTypeLocalized()
	{
		return null;
	}

	public static string GetBallTypeCapitalized()
	{
		return null;
	}

	public PremierLeagueScavengerHunt()
		: base(HOLIDAY_COLLECTIBLE_TYPE.NONE)
	{
	}

	public override IEnumerator<ScavengerItem> GetEnumerator()
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

	public override DataAccessory GetCompletionAccessory()
	{
		return null;
	}
}
