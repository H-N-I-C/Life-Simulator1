using System;
using System.Collections;

[Serializable]
public class PersonEmojiScript
{
	private int SkinColor__BackingField;

	private int Gender__BackingField;

	private int PermanentMood__BackingField;

	private ArrayList EmojiArray__BackingField;

	public string SaveDataString;

	[NonSerialized]
	private VoltronFaceContainer mContainer;

	public int SkinColor
	{
		get
		{
			return SkinColor__BackingField;
		}
		set
		{
			SkinColor__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public int PermanentMood
	{
		get
		{
			return PermanentMood__BackingField;
		}
		set
		{
			PermanentMood__BackingField = value;
		}
	}

	public ArrayList EmojiArray
	{
		get
		{
			return EmojiArray__BackingField;
		}
		set
		{
			EmojiArray__BackingField = value;
		}
	}

	public PersonEmojiScript()
	{
	}

	public PersonEmojiScript(int skinColor, int gender, int age, int mood, DataCountry country, bool canBeCrazy)
	{
	}

	public PersonEmojiScript(int theSkinColor, int theGender)
	{
	}

	public PersonEmojiScript(string pData)
	{
	}

	public PersonEmojiScript(SimPerson parent1, SimPerson parent2, int gender, bool pAllowDyedHair)
	{
	}

	public string GetData()
	{
		return null;
	}

	public void SetDataWithImageUpdate(string pData, bool pUpdate)
	{
	}

	public string GetImageCacheID()
	{
		return null;
	}

	public int GetSkinColor()
	{
		return 0;
	}

	public int GetNaturalHairColorType()
	{
		return 0;
	}

	public int GetAgeToStartWearingGlasses()
	{
		return 0;
	}

	public int GetAgeToStartWearingContacts()
	{
		return 0;
	}

	public bool GetWearingGlassesAndLimitedToPrescriptionOnly(bool pLimitedToPrescriptionOnly)
	{
		return false;
	}

	public void SetGender(int pGender)
	{
	}

	public void ChangeSex()
	{
	}

	public void SetAgeToStartWearingGlasses(int pAge)
	{
	}

	public void SetAgeToStartWearingContacts(int pAge)
	{
	}

	public void SetWearingGlassesWithImageUpdate(bool pWearingGlasses, bool pUpdate)
	{
	}

	public void SetGlassesType(int index)
	{
	}

	public void SetHeadwearType(int index)
	{
	}

	private void SetGenderWithImageUpdate(int pGender, bool pUpdate)
	{
	}

	private void SetVoltronMoodWithImageUpdate(int mood, bool pUpdate)
	{
	}

	private void SetAgeWithImageUpdate(int pAge, bool pUpdate)
	{
	}

	public PersonEmojiScript GetVariantEmojiScript(int pAge, int pGender)
	{
		return null;
	}

	private void ShuffleSurfaceLevelFeatures()
	{
	}

	public void OverrideImageCacheID(string pImageCacheID)
	{
	}

	public bool ShowingHijab()
	{
		return false;
	}

	public void SetMoodWithImageUpdate(int mood, bool pUpdate)
	{
	}

	private bool IsWearingVampireLook()
	{
		return false;
	}

	public void SyncVampireLookForPerson(SimPerson person)
	{
	}

	public void ApplyVampireLook()
	{
	}

	public void RemoveVampireLook()
	{
	}

	private bool AdjustFacialHairToComplyWithVampireLook()
	{
		return false;
	}

	public void AddRandomDisguise(bool isVampire)
	{
	}

	public void AddRandomDrugAddictDisguise(bool isCop, bool overrideWithVampireDisguise)
	{
	}

	public void RemoveDisguiseWithImageUpdate(bool pUpdate, bool keepVampireDisguise = true)
	{
	}

	private void RemoveVampireDisguiseWithImageUpdate(bool pUpdate)
	{
	}

	private bool HasVampireDisquise()
	{
		return false;
	}

	public bool HasDisguise()
	{
		return false;
	}

	private int ConvertSimMoodToVoltronMood(int mood)
	{
		return 0;
	}

	public string EmojiStringForPerson(SimPerson pPerson)
	{
		return null;
	}

	private string EmojiStringForPersonUnguarded(SimPerson pPerson)
	{
		return null;
	}

	public string EmojiForCurrentState()
	{
		return null;
	}

	public string EmojiForGhostState()
	{
		return null;
	}

	public string EmojiForGrayscaleState()
	{
		return null;
	}

	public PersonEmojiScript GetDuplicateEmojiScript()
	{
		return null;
	}

	private VoltronFaceContainer MakeContainerFromSkinColorGenderEmojiArray(int pSkinColor, int pGender, ArrayList pEmojiArray)
	{
		return null;
	}

	public void CopyGenderFromContainer()
	{
	}

	public static bool CountryRequiresHijab(DataCountry theCountry)
	{
		return false;
	}

	public static int MoodForFriendliness(int friendliness)
	{
		return 0;
	}

	public void VerifyContainerLoaded()
	{
	}

	private void ReportCorruptEmoji(string origin, string reason, Exception e)
	{
	}

	public bool ContainsEmojiData()
	{
		return false;
	}

	public void UncenteredImageUpdate()
	{
	}
}
