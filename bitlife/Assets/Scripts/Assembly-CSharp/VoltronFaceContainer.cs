using System.Collections;

public class VoltronFaceContainer : VoltronContainer
{
	protected bool mGhosted;

	protected bool mGrayscaled;

	public VoltronFaceContainer(int pSkinType, DataCountry pCountry, int pMood, int pAge, int pGender, bool pCanBeCrazy)
	{
	}

	public VoltronFaceContainer(int pSkinType, int pGender, ArrayList pEmojiArray)
	{
	}

	public VoltronFaceContainer()
	{
	}

	public VoltronFaceContainer(string pData)
	{
	}

	public override void CreateComponentForFeature(int pFeature)
	{
	}

	public void SetMood(int pMood)
	{
	}

	public int GetMood()
	{
		return 0;
	}

	private void ApplyHairType()
	{
	}

	public bool GetShowingHijab()
	{
		return false;
	}

	public void SetShowHijab(bool pShow)
	{
	}

	public bool GetWearingGlasses()
	{
		return false;
	}

	public void SyncGlassesType()
	{
	}

	public void SyncForManualGlassesChange()
	{
	}

	public void SyncForManualHairChange()
	{
	}

	public void SyncForManualFacialHairChange()
	{
	}

	public void ManualDisableSomeHairAttributes()
	{
	}

	public void SyncForManualSkinColorChange()
	{
	}

	public int GetSkinType()
	{
		return 0;
	}

	public int GetGender()
	{
		return 0;
	}

	public void SetGender(int pGender)
	{
	}

	public void SetGenderWithNoSwap(int pGender)
	{
	}

	public string GetNaturalHairColor()
	{
		return null;
	}

	public void UpdateImage(bool centered = true)
	{
	}

	public void GhostImage()
	{
	}

	public void GrayscaleImage()
	{
	}
}
