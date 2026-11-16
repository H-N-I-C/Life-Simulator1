using System;
using System.Collections.Generic;
using Game;

public class DrinkBloodActivity
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataVampireFeedingLocation, string> _003C_003E9__70_0;

		public static Func<DataVampireFeedingLocation, int> _003C_003E9__87_1;

		internal string _003C_002Ector_003Eb__70_0(DataVampireFeedingLocation locationData)
		{
			return null;
		}

		internal int _003CSelectLocation_003Eb__87_1(DataVampireFeedingLocation locationData)
		{
			return 0;
		}
	}

	private const int MinWilinessToArgue = 50;

	private const int MinRelationshipToRefuse = 36;

	private const int ImpactFailedHypnotizeRelationship = -3;

	private const int ImpactFeedingHappiness = 1;

	private const int ImpactFeedingNotoriety = 1;

	private const int ImpactFailAnimalScapeRelationship = -2;

	private const int ImpactAnimalScapeNotoriety = 1;

	private const int ImpactArgumentRelationship = -3;

	private const int ImpactArgumentScapeNotoriety = 1;

	private const int ImpactDrainedToDeathHappiness = 2;

	private const int ImpactDrainedToDeathNotoriety = 2;

	private const int ImpactTurnVictimIntoVampireHappiness = 2;

	private const int ImpactTurnVictimIntoVampireNotoriety = -1;

	private const int ImpactHunterSeducedHappiness = 1;

	private const int ImpactHunterSeducedEssence = -1;

	private const int ImpactHunterSeducedNotoriety = -1;

	private const int ImpactHunterFleeSuccessNotoriety = 2;

	private const int ImpactVColiSmarts = -2;

	private const int ImpactVColiHappiness = -2;

	private SimPerson Victim__BackingField;

	private string SelectedLocation__BackingField;

	private string LocationColorString__BackingField;

	private string LocationColorLog__BackingField;

	private int LocationRiskScore__BackingField;

	private bool IsVictimFullyDrainedNextAttack__BackingField;

	private bool IsBloodContaminated__BackingField;

	private string BloodTaste__BackingField;

	private bool IsVictimFamiliar__BackingField;

	private bool _includeConversion;

	private SimVampire Vampire__BackingField;

	private SimHero _hero;

	private List<DataVampireFeedingLocation> LocationSet__BackingField;

	private int BloodSuckCount__BackingField;

	public readonly BracketParser BracketParser;

	public SimPerson Victim
	{
		get
		{
			return Victim__BackingField;
		}
		private set
		{
			Victim__BackingField = value;
		}
	}

	public string SelectedLocation
	{
		get
		{
			return SelectedLocation__BackingField;
		}
		private set
		{
			SelectedLocation__BackingField = value;
		}
	}

	public string LocationColorString
	{
		get
		{
			return LocationColorString__BackingField;
		}
		private set
		{
			LocationColorString__BackingField = value;
		}
	}

	public string LocationColorLog
	{
		get
		{
			return LocationColorLog__BackingField;
		}
		private set
		{
			LocationColorLog__BackingField = value;
		}
	}

	public int LocationRiskScore
	{
		get
		{
			return LocationRiskScore__BackingField;
		}
		set
		{
			LocationRiskScore__BackingField = value;
		}
	}

	public bool IsVictimFullyDrainedNextAttack
	{
		get
		{
			return IsVictimFullyDrainedNextAttack__BackingField;
		}
		private set
		{
			IsVictimFullyDrainedNextAttack__BackingField = value;
		}
	}

	public bool IsBloodContaminated
	{
		get
		{
			return IsBloodContaminated__BackingField;
		}
		private set
		{
			IsBloodContaminated__BackingField = value;
		}
	}

	public string BloodTaste
	{
		get
		{
			return BloodTaste__BackingField;
		}
		private set
		{
			BloodTaste__BackingField = value;
		}
	}

	public bool IsVictimFamiliar
	{
		get
		{
			return IsVictimFamiliar__BackingField;
		}
		private set
		{
			IsVictimFamiliar__BackingField = value;
		}
	}

	public SimVampire Vampire
	{
		get
		{
			return Vampire__BackingField;
		}
		private set
		{
			Vampire__BackingField = value;
		}
	}

	public List<DataVampireFeedingLocation> LocationSet
	{
		get
		{
			return LocationSet__BackingField;
		}
		private set
		{
			LocationSet__BackingField = value;
		}
	}

	public int BloodSuckCount
	{
		get
		{
			return BloodSuckCount__BackingField;
		}
		private set
		{
			BloodSuckCount__BackingField = value;
		}
	}

	public DrinkBloodActivity(SimPerson person)
	{
	}

	public void SuckBlood()
	{
	}

	public bool CanTurnThemIntoVampire()
	{
		return false;
	}

	public bool VampireHunterAppears()
	{
		return false;
	}

	public void DrainBlood()
	{
	}

	public bool VictimHaveEnoughBlood()
	{
		return false;
	}

	public bool VictimArgueWithVampire()
	{
		return false;
	}

	public bool RelationshipStrongEnough()
	{
		return false;
	}

	public void ImpactFromFailHypnotize()
	{
	}

	public void ImpactFromBloodSucking()
	{
	}

	public void ImpactAnimalScape(bool failed)
	{
	}

	public void ImpactArgument()
	{
	}

	public void ImpactDrainedToDeath()
	{
	}

	public void ImpactTurnVictimIntoVampire()
	{
	}

	public static void ImpactHunterSeduced()
	{
	}

	public static void ImpactHunterFleeSuccess()
	{
	}

	public void ImpactGotVColi()
	{
	}

	public void SelectLocation(string selected)
	{
	}

	public bool GetDisease()
	{
		return false;
	}

	private bool _003CSelectLocation_003Eb__87_0(DataVampireFeedingLocation locationData)
	{
		return false;
	}
}
