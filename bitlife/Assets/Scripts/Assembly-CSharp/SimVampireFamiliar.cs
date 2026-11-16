using System;

[Serializable]
public class SimVampireFamiliar : SimLover
{
	private int AgeBecameFamiliar__BackingField;

	private int HeroAgeAtLastInteraction__BackingField;

	public int AgeBecameFamiliar
	{
		get
		{
			return AgeBecameFamiliar__BackingField;
		}
		set
		{
			AgeBecameFamiliar__BackingField = value;
		}
	}

	public int HeroAgeAtLastInteraction
	{
		get
		{
			return HeroAgeAtLastInteraction__BackingField;
		}
		set
		{
			HeroAgeAtLastInteraction__BackingField = value;
		}
	}

	public SimVampireFamiliar(bool initHere)
		: base(initHere: false)
	{
	}

	public SimVampireFamiliar(SimPerson otherPerson)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	public override void BecomeVampire(int infectedAge, bool shouldGetVampireName, SimPerson sourceVampire = null)
	{
	}

	private void TryReleaseFromHero(SimHero hero)
	{
	}
}
