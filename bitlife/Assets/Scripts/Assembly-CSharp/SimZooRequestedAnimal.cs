using System;

[Serializable]
public class SimZooRequestedAnimal
{
	private string DesiredBreed__BackingField;

	private float DesiredAnimalQualityLevel__BackingField;

	private GENDER DesiredGender__BackingField;

	private double OfferPrice__BackingField;

	private string RequestingZooName__BackingField;

	private SimZooCompetitor _zoo;

	public string DesiredBreed
	{
		get
		{
			return DesiredBreed__BackingField;
		}
		set
		{
			DesiredBreed__BackingField = value;
		}
	}

	public float DesiredAnimalQualityLevel
	{
		get
		{
			return DesiredAnimalQualityLevel__BackingField;
		}
		set
		{
			DesiredAnimalQualityLevel__BackingField = value;
		}
	}

	public GENDER DesiredGender
	{
		get
		{
			return DesiredGender__BackingField;
		}
		set
		{
			DesiredGender__BackingField = value;
		}
	}

	public double OfferPrice
	{
		get
		{
			return OfferPrice__BackingField;
		}
		set
		{
			OfferPrice__BackingField = value;
		}
	}

	public string RequestingZooName
	{
		get
		{
			return RequestingZooName__BackingField;
		}
		set
		{
			RequestingZooName__BackingField = value;
		}
	}

	public SimZooRequestedAnimal(SimZooCompetitor zoo, string breed = null, GENDER gender = GENDER.BOTH, double offerPrice = 0.0)
	{
	}

	private double GetPurchaseRequestOfferPrice()
	{
		return 0.0;
	}
}
