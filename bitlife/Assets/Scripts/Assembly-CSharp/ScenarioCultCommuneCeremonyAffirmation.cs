using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioCultCommuneCeremonyAffirmation : Scenario
{
	private string _narrative;

	private SimCult _cult;

	private float _max;

	private List<SimCommunard> _simCommunards;

	private List<DataCultAffirmationFilmTitle> _possibleTitles;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
