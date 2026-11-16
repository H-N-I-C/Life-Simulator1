using UnityEngine;

public class VoltronFaceContainerView : VoltronContainerView
{
	public void InitWithParameters(Vector2 pSize, int pSkinType, DataCountry pCountry, int pMood, int pAge, int pGender, bool pCanBeCrazy)
	{
	}

	public VoltronFaceContainer GetContainer()
	{
		return null;
	}

	public void BuildContainerWithSkinTypeCountryMoodAgeGenderCanBeCrazy(int pSkinType, DataCountry pCountry, int pMood, int pAge, int pGender, bool pCanBeCrazy)
	{
	}

	public override void BuildDefaultSettingsContainer()
	{
	}

	public override void BuildContainerWithData(string pData)
	{
	}

	public override float GetAlphaForFeature(int pFeature)
	{
		return 0f;
	}

	public override string GetCurrentFolderForFeature(int pFeature)
	{
		return null;
	}
}
