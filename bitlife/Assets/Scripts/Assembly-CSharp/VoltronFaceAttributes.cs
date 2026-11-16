using System.Collections;

public class VoltronFaceAttributes
{
	public int mStage;

	public int mGender;

	public int mSkinType;

	public int mMood;

	public int mHairType;

	public int mAgedHairColorType;

	public int mAgeToStartWearingPrescriptionGlasses;

	public int mAgeToStartWearingPrescriptionContacts;

	public bool mWillHaveFacialHair;

	public bool mWillDyeHair;

	public bool mWillGoBald;

	public bool mWillShaveHead;

	public bool mRequiresHijab;

	public bool mWearingGlasses;

	public ArrayList mAccessories;

	public ArrayList mDisguises;

	private static string lgGeneralHairColor;

	public VoltronFaceAttributes()
	{
	}

	private bool CountryAllowsCurlyHair(DataCountry pCountry)
	{
		return false;
	}

	public VoltronFaceAttributes(int pSkinType, DataCountry pCountry, int pMood, int pGender, bool pCanBeCrazy, ArrayList pExtractedFeatureColors)
	{
	}

	public VoltronFaceAttributes(int pSkinType, int pGender, ArrayList pEmojiArray, ArrayList pExtractedFeatureColors)
	{
	}

	public void SwitchGender()
	{
	}

	private string GetStartingColorForFeatureUsingSkinTypeCountryUseOldStyle(int pFeature, int pSkinType, DataCountry pCountry, bool pOldStyle)
	{
		return null;
	}

	public string PickHairColorUsingSkinTypeUseOldStyle(int pSkinType, bool pOldStyle)
	{
		return null;
	}

	private void SetDefaults()
	{
	}

	public string GetData()
	{
		return null;
	}

	public void SetData(string pData)
	{
	}
}
