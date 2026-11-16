using System.Collections.Generic;

public class ScenarioAttackInbound : Scenario
{
	protected string _narrative;

	protected string _whoString;

	protected string _whoStringNarrative;

	protected bool _attackerIsBully;

	protected bool _attackerIsSibling;

	protected bool _attackerIsPrisoner;

	protected DataWeapon _weapon;

	protected SimClassmate _bully;

	protected SimPerson _friend;

	protected DataHitman _hitman;

	protected int _attackerGender;

	protected SimPerson _person;

	protected int _heroDeathOverride;

	private SimSecretAgent _secretAgent;

	private SimDealerStreetPerson _streetPerson;

	public ScenarioAttackInbound(SimPerson thePerson, int gender, string who, bool retaliatory, int heroDeathOverride = 0)
	{
	}

	public ScenarioAttackInbound(SimSecretAgent thePerson, int heroDeathOverride = 0)
	{
	}

	public ScenarioAttackInbound(SimClassmate theBully)
	{
	}

	public ScenarioAttackInbound(SimSibling theSibling)
	{
	}

	public ScenarioAttackInbound(SimPrisoner thePrisoner)
	{
	}

	public ScenarioAttackInbound(SimPerson theFriend, DataWeapon theWeapon)
	{
	}

	public ScenarioAttackInbound(DataHitman theHitman, DataWeapon theWeapon)
	{
	}

	public ScenarioAttackInbound(SimDealerStreetPerson thePerson, int heroDeathOverride = 0)
	{
	}

	public ScenarioAttackInbound(SimPerson thePerson, List<string> customTitles, string customAttackerDescription, string customNarrative, string customExteriorTitle)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool _003CRefreshScenario_003Eb__24_0(SimPerson t)
	{
		return false;
	}

	private bool _003CChoiceSelectedAtIndex_003Eb__26_0(SimModel x)
	{
		return false;
	}
}
