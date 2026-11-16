using System;

[Serializable]
public class SimWorld
{
	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public Random rand;

		internal int _003CGetEligibleSiblingForHeroFiller_003Eb__0(SimPerson a)
		{
			return 0;
		}
	}

	private SimWar War__BackingField;

	public SimWar War
	{
		get
		{
			return War__BackingField;
		}
		set
		{
			War__BackingField = value;
		}
	}

	public void IncrementAgeAndSuppressLog(bool suppress)
	{
	}

	public string GetStringIfEligibleForHeroFiller(DataHeroFiller heroFiller)
	{
		return null;
	}

	public SimPerson GetEligibleChildForHeroFiller(DataHeroFiller heroFiller)
	{
		return null;
	}

	public SimPerson GetEligibleGrandchildForHeroFiller(DataHeroFiller heroFiller)
	{
		return null;
	}

	public SimPerson GetEligibleSiblingForHeroFiller(DataHeroFiller heroFiller)
	{
		return null;
	}

	public bool IsEligibleSubjectOfHeroFillerForPersonWithRelationshipStrength(DataHeroFiller heroFiller, SimPerson person, float relationshipStrength)
	{
		return false;
	}

	public string GetStringForPetReleaseFillerStringAndPet(string petReleaseFillerString, SimPet pet)
	{
		return null;
	}
}
