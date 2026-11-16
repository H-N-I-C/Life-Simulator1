using System.Collections;
using System.Collections.Generic;

public class ScenarioVampireMakerOfferSacrifice : Scenario
{
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public ScenarioVampireMakerOfferSacrifice _003C_003E4__this;

		public string dropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimPerson person)
		{
			return false;
		}
	}

	private readonly SimPerson _maker;

	private readonly SimHero _hero;

	private string _narrative;

	private List<SimPerson> _sacrifices;

	private SimPerson _chosenSacrifice;

	private readonly HeroKillingAnNPC _heroKillingAnNPC;

	public ScenarioVampireMakerOfferSacrifice(SimPerson maker, List<SimPerson> sacrifices)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private string RelationshipToHero(SimPerson person)
	{
		return null;
	}

	private string _003CRefreshScenario_003Eb__7_0(SimPerson x)
	{
		return null;
	}
}
