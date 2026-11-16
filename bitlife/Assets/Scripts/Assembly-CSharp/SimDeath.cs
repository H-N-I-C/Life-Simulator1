using System;
using Game;

[Serializable]
public class SimDeath
{
	private int DeathType__BackingField;

	private string DescriptiveSentence__BackingField;

	[NonSerialized]
	public DataDrug OverdoseDrug;

	[NonSerialized]
	public SimDisease FatalDisease;

	[NonSerialized]
	public DataAnimal KillerAnimal;

	[NonSerialized]
	public DataFightErAnimal FighterKillerAnimal;

	[NonSerialized]
	public DataWitchRemedy FatalWitchRemedy;

	[NonSerialized]
	public DataPlasticSurgery FatalPlasticSurgery;

	[NonSerialized]
	public SimPet KillerPet;

	public string KilledWithWeaponString;

	private int AgeAtDeath__BackingField;

	private bool DiedAHero__BackingField;

	private double Inheritence__BackingField;

	public int DeathType
	{
		get
		{
			return DeathType__BackingField;
		}
		set
		{
			DeathType__BackingField = value;
		}
	}

	public string DescriptiveSentence
	{
		get
		{
			return DescriptiveSentence__BackingField;
		}
		set
		{
			DescriptiveSentence__BackingField = value;
		}
	}

	public DataWeapon KilledWithWeapon
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtDeath
	{
		get
		{
			return AgeAtDeath__BackingField;
		}
		set
		{
			AgeAtDeath__BackingField = value;
		}
	}

	public bool DiedAHero
	{
		get
		{
			return DiedAHero__BackingField;
		}
		set
		{
			DiedAHero__BackingField = value;
		}
	}

	public double Inheritence
	{
		get
		{
			return Inheritence__BackingField;
		}
		set
		{
			Inheritence__BackingField = value;
		}
	}

	public SimDeath()
	{
	}

	public SimDeath(int theDeathType)
	{
	}

	public void UpdateDeathType(DEATH_TYPE deathType)
	{
	}

	public void UpdateDeathType(int theDeathType)
	{
	}

	public void UpdateWithFlukeAtAge(int age)
	{
	}

	public void UpdateWithFatalDisease(SimDisease theDisease)
	{
	}

	public void UpdateWithKillerAnimal(DataAnimal theAnimal)
	{
	}

	public void UpdateWithKillerAnimal(DataFightErAnimal animal)
	{
	}

	public void UpdateWithOutdoorCreature(SimOutdoorAnimal theAnimal)
	{
	}

	public void UpdateWithOutdoorCreature(SimOutdoorMarineLife theAnimal)
	{
	}

	public void UpdateWithOutdoorCreature(SimOutdoorBird theAnimal)
	{
	}

	public void UpdateWithOutdoorPlant(SimOutdoorPlant plant)
	{
	}

	public void UpdateWithFatalWitchRemedy(DataWitchRemedy theWitchRemedy)
	{
	}

	public void UpdateWithFatalPlasticSurgery(DataPlasticSurgery thePlasticSurgery)
	{
	}

	public void UpdateWithKillerPet(SimPet thePet)
	{
	}

	public void UpdateWithKilledInSelfDefense()
	{
	}

	public void UpdateWithDeadlyAssault()
	{
	}

	public void UpdateWithDeadlyAssaultWithWeapon(DataWeapon theWeapon)
	{
	}

	public void UpdateWithWarZoneDeath()
	{
	}

	public void UpdateWithMilitaryDeploymentDeathOnDeploymentAndMinesweeperTheme(string deploymentString, int theme)
	{
	}

	public void UpdateWithDeathPenaltyMethod(string method)
	{
	}

	public void UpdateWithBeatenByGangName(string gangName)
	{
	}

	public void UpdateWithBeatenBySchoolCliqueNameInSchoolTypeString(string cliqueName, string schoolType)
	{
	}

	public void UpdateWithLightningStrike()
	{
	}

	public void UpdateWithChokeOnFood(string food)
	{
	}

	public void UpdateWithRunOverByTrainAtTime(string trainName, string trainTime)
	{
	}

	public void UpdateWithCrashInAircraft(DataAircraft aircraft)
	{
	}

	public void UpdateWithAssassinationWhileServingInPoliticalRole(string role)
	{
	}

	public void UpdateWithRoyalExecutionMethod(DataRoyalExecution method)
	{
	}

	public void UpdateWithMafiaExecutionMethod(DataMafiaExecution method)
	{
	}

	public void UpdateWithRandomMafiaExecutionMethod()
	{
	}

	public void UpdateWithBeatenByMafiaFamilyName(string familyName)
	{
	}

	public void UpdateWithKilledByAct(bool practice, DataHustlerPerformAct act)
	{
	}

	public void UpdateWithSuicide()
	{
	}

	public bool IsDeathTypeFailedRescue()
	{
		return false;
	}
}
