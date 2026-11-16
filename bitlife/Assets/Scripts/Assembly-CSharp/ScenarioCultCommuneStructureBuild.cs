using System;
using System.Collections;
using Game;

public class ScenarioCultCommuneStructureBuild : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataCultCommuneStructure, string> _003C_003E9__1_0;

		internal string _003CRefreshScenario_003Eb__1_0(DataCultCommuneStructure x)
		{
			return null;
		}
	}

	private SimCult _cult;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
