using System;
using System.Collections.Generic;
using System.Linq;

public class ISAdQualitySegment
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<string, string>, string> _003C_003E9__10_0;

		public static Func<IGrouping<string, KeyValuePair<string, string>>, string> _003C_003E9__10_1;

		public static Func<IGrouping<string, KeyValuePair<string, string>>, string> _003C_003E9__10_2;

		internal string _003CgetSegmentAsDict_003Eb__10_0(KeyValuePair<string, string> d)
		{
			return null;
		}

		internal string _003CgetSegmentAsDict_003Eb__10_1(IGrouping<string, KeyValuePair<string, string>> d)
		{
			return null;
		}

		internal string _003CgetSegmentAsDict_003Eb__10_2(IGrouping<string, KeyValuePair<string, string>> d)
		{
			return null;
		}
	}

	public string name;

	public int age;

	public string gender;

	public int level;

	public int isPaying;

	public double inAppPurchasesTotal;

	public long userCreationDate;

	public Dictionary<string, string> customs;

	public void setCustom(string key, string value)
	{
	}

	public Dictionary<string, string> getSegmentAsDict()
	{
		return null;
	}
}
