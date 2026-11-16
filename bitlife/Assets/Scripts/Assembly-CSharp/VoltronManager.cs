using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Containers;
using UnityEngine;

public class VoltronManager
{
	private sealed class _003CILoadListItemEmoji_003Ed__66 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VoltronManager _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CILoadListItemEmoji_003Ed__66(int _003C_003E1__state)
		{
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
		}
	}

	public const string HISTORY_SEPARATOR = "[H]";

	public const string COMPONENT_SEPARATOR = "[C]";

	public const string ATTRIBUTE_SEPARATOR = "[A]";

	public const string DETAILS_SEPARATOR = ":";

	public const string CONTAINER_CONTENT_SEPARATOR = ",";

	public const string ACCESSORY_SEPARATOR = "#";

	public const string DISGUISE_SEPARATOR = "#";

	public const int NONE = -1;

	public const int NUMBER_OF_STAGES = 6;

	public const string FILE_PREFIX = "a16_faces";

	public const string DISGUISE_FILE_PREFIX = "a16_disguise";

	public const string EMOJI_FACES_RESOURCES_FOLDER = "emoji/Voltron/faces";

	public const string EMOJI_ACCESSORIES_RESOURCES_FOLDER = "emoji/Voltron/accessories";

	public const string EMOJI_DISGUISES_RESOURCES_FOLDER = "emoji/Voltron/disguises";

	public const int IMAGE_SIZE = 200;

	public const int NUMBER_OF_EMOJI_GENDERS = 2;

	public const int CHANCE_FEMALE_DYES_HAIR = 5;

	public const int CHANCE_MALE_DYES_HAIR = 3;

	public const int ACCESSORY_LAYER_INDEXING_OFFSET = 1000;

	public const int DISGUISE_LAYER_INDEXING_OFFSET = 2000;

	public const string DISGUISE_DEFAULT_COLOR = "ffffff";

	public const int DISGUISE_MINIMUM_STAGE = 2;

	private long mStartID;

	private ArrayList mVoltronFiles;

	private ArrayList mDisguiseFiles;

	private ArrayList mTempSortArray;

	private ArrayList mFiles;

	private ArrayList mFeatureSupportData;

	private ArrayList mColorPalettes;

	private Dictionary<string, VoltronImageCacheItem> mImageCache;

	private Dictionary<string, string> mIDBlackList;

	private ArrayList mListItemEmojiCache;

	private GameObject mFaceContainerPrefabObject;

	private GameObject mLayerPrefabObject;

	private bool mListItemCacheItemRemovalInProgress;

	private bool mListItemCacheItemAdditionInProgress;

	private static GameObject lTestObject;

	private static VoltronManager SharedInstance;

	private readonly Dictionary<string, RenderTexture> _cache;

	public static VoltronManager SharedVoltronManager => null;

	public void Initialize()
	{
	}

	private void BuildDisguiseDatabase()
	{
	}

	private void BuildSupportData()
	{
	}

	private void BuildIDBlackList()
	{
	}

	private GameObject InstantiateVoltronFaceContainer()
	{
		return null;
	}

	public GameObject InstantiateVoltronLayer()
	{
		return null;
	}

	public GameObject CreateFaceContainerViewWithSizeSkinTypeCountryMoodAgeGenderCanBeCrazy(Vector2 pSize, int pSkinType, DataCountry pCountry, int pMood, int pAge, int pGender, bool pCanBeCrazy)
	{
		return null;
	}

	public GameObject CreateFaceContainerViewWithSizeData(Vector2 pSize, string pData)
	{
		return null;
	}

	public Sprite GetCenteredSprite(Sprite uncenteredSprite)
	{
		return null;
	}

	public void RemoveHalo(Color32[] pData)
	{
	}

	public Rect RectForCroppedTextureData(Color32[] pData, int pWidth, int pHeight)
	{
		return default;
	}

	public Color32[] CenterDataWithEdgeRect(Color32[] pData, Rect pEdgeRect, int pWidth, int pHeight)
	{
		return null;
	}

	public void UpdateCacheWithImageID(Sprite pImage, string pID)
	{
	}

	public void UpdateCacheWithDataID(string pData, string pID)
	{
	}

	public void RemoveImageFromCacheWithID(string pID)
	{
	}

	public bool ImageCacheContainsID(string pID)
	{
		return false;
	}

	public Sprite GetImageFromImageCacheWithID(string pID, bool centered = true)
	{
		return null;
	}

	public string GetDataFromImageCacheWithID(string pID)
	{
		return null;
	}

	public bool ImageCacheContainsPrebuiltImageWithID(string pID)
	{
		return false;
	}

	public bool ImageCacheContainsIDWithUnbuiltImage(string id)
	{
		return false;
	}

	public void ClearImageCache()
	{
	}

	public void RemoveStaleImagesFromCache()
	{
	}

	public string GenerateUniqueCacheID()
	{
		return null;
	}

	public Texture2D CopyTexture(Texture2D lTexture, int lTargetX, int lTargetY)
	{
		return null;
	}

	public void InitiateListItemEmojiCacheCoroutine()
	{
	}

	public IEnumerator ILoadListItemEmoji()
	{
		return null;
	}

	public void LoadNextListItemEmoji()
	{
	}

	private bool IsUnbuiltCompositedEmoji(string id)
	{
		return false;
	}

	public void RemoveListItemsFromCache()
	{
	}

	public void AddListItemToCache(UIListItem pListItem, string pID)
	{
	}

	public void RemoveListItemEmojiFromCacheForID(string pID)
	{
	}

	public void RemoveListItemEmojiFromCacheForListItem(UIListItem pListItem)
	{
	}

	private void RemoveItemFromListItemCache(VoltronListItemCacheItem pItem)
	{
	}

	public Sprite RequestEmojiSpriteForListItem(UIListItem pListItem, string pID)
	{
		return null;
	}

	public bool ListItemCacheContainsListItem(UIListItem pListItem)
	{
		return false;
	}

	public string GetFolderForFeature(int pFeature)
	{
		return null;
	}

	public string GetFolderForFeatureStageGender(int pFeature, int pStage, int pGender)
	{
		return null;
	}

	public string GetFolderForGender(int pGender)
	{
		return null;
	}

	public string StripIDAndExtensionOffFilename(string pFilename)
	{
		return null;
	}

	public ArrayList GetEntriesForStageFeatureGender(int pStage, int pFeature, int pGender)
	{
		return null;
	}

	public ArrayList GetDisguiseEntriesForStageTypeGender(int pStage, int pType, int pGender)
	{
		return null;
	}

	public void SortFeatureStyles()
	{
	}

	public void SortDisguiseFeatureStyles()
	{
	}

	public void SortFileEntries(ArrayList pFileEntries)
	{
	}

	private void SortDisguiseFileEntries(ArrayList pFileEntries)
	{
	}

	public VoltronFaceFileEntry FindFileEntryInArrayStageFeatureGenderMoodVersion(ArrayList pArray, int pStage, int pFeature, int pGender, int pMood, int pVersion)
	{
		return null;
	}

	public VoltronFaceDisguiseFileEntry FindDisguiseFileEntryInArrayTypeStyle(ArrayList pArray, int pType, int pStyle)
	{
		return null;
	}

	public string GetDisguiseFilenameForStageTypeGenderStyle(int pStage, int pType, int pGender, int pStyle)
	{
		return null;
	}

	public string GetFilenameSegmentForGender(int pGender)
	{
		return null;
	}

	public string GetFilenameSegmentForGenderStageFeatureMoodVersion(int pGender, int pStage, int pFeature, int pMood, int pVersion)
	{
		return null;
	}

	public string GetFilenameSegmentForMoodStageFeature(int pMood, int pStage, int pFeature)
	{
		return null;
	}

	public string GetFilenameSegmentForFeature(int pFeature)
	{
		return null;
	}

	public string GetNameForStyleFeatureStage(int pStyle, int pFeature, int pStage)
	{
		return null;
	}

	public int GetRandomStyleForHairTypeStageGenderFeature(int pHairType, int pStage, int pGender, int pFeature)
	{
		return 0;
	}

	public int GetRandomGlassesStyleForStageGender(int pStage, int pGender)
	{
		return 0;
	}

	public int GetHijabStyleForStageGenderFeature(int pStage, int pGender, int pFeature)
	{
		return 0;
	}

	public int GetStyleForMoodStageGenderFeatureCurrentStyle(int pMood, int pStage, int pGender, int pFeature, int pCurrentStyle)
	{
		return 0;
	}

	public int GetNumberOfMoodVersionsForFeature(int pFeature, int pGender, int pStage)
	{
		return 0;
	}

	public ArrayList GetArrayOfAssociatedFeaturesForFeature(int pFeature)
	{
		return null;
	}

	public ArrayList GetArrayOfSubmissiveFeaturesForFeature(int pFeature)
	{
		return null;
	}

	public string GetDefaultColorForFeature(int pFeature)
	{
		return null;
	}

	public int GetDefaultStyleForFeature(int pFeature)
	{
		return 0;
	}

	public bool MoodCapableFeatureStageGender(int pFeature, int pStage, int pGender)
	{
		return false;
	}

	public bool IsOptionalFeature(int pFeature)
	{
		return false;
	}

	public bool ShouldDarkenColor(int pFeature)
	{
		return false;
	}

	public int GetLayerPositionForFeature(int pFeature)
	{
		return 0;
	}

	public bool ShouldExposeOnlyOneStyleForFeature(int pFeature)
	{
		return false;
	}

	public int GetEditableRuleForFeature(int pFeature)
	{
		return 0;
	}

	public bool GetcolorizableForFeature(int pFeature)
	{
		return false;
	}

	public string GetSimpleNameForFeature(int pFeature)
	{
		return null;
	}

	public int GetFeatureForSimpleName(string pSimpleName)
	{
		return 0;
	}

	public int MinimumStageForFacialHair()
	{
		return 0;
	}

	public int MinimumStageForDyedHair()
	{
		return 0;
	}

	public int MinimumStageForAgedHairColor()
	{
		return 0;
	}

	public int MinimumStageForBaldness()
	{
		return 0;
	}

	public int MinimumStageForShavedHead()
	{
		return 0;
	}

	public int MinimumStageForHijab()
	{
		return 0;
	}

	public int GetStageForAge(int pAge)
	{
		return 0;
	}

	public int GetMinimumAgeForStage(int pStage)
	{
		return 0;
	}

	public int GetStyleWithStageFeatureGenderTheId(int pStage, int pFeature, int pGender, string pID)
	{
		return 0;
	}

	public static string ColorToHex(Color pColor)
	{
		return null;
	}

	public int GetHairColorTypeForColor(string pColor)
	{
		return 0;
	}

	public string GetClosestColorToColorForFeature(string pColor, int pFeature)
	{
		return null;
	}

	public Color GetColorFromHexString(string pHexString)
	{
		return default;
	}

	public ArrayList GetColorPaletteForFeature(int pFeature)
	{
		return null;
	}

	public int GetColorPaletteIDForFeature(int pFeature)
	{
		return 0;
	}

	public bool ColorExistsInPalette(string pColor, int pPalette)
	{
		return false;
	}

	public int GetIndexForColorInPalette(string pColor, int pPaletteID)
	{
		return 0;
	}

	public string GetColorForIndexInPalette(int pIndex, int pPaletteID)
	{
		return null;
	}

	public string GetColorForIndexInPaletteForFeature(int pIndex, int pFeature)
	{
		return null;
	}

	public ArrayList GetColorPaletteWithID(int pPalletID)
	{
		return null;
	}

	public string GetAveragedSkinColorFromColor1Color2(string pColor1, string pColor2)
	{
		return null;
	}

	public string GetRandomColorFromPalette(int pPalette)
	{
		return null;
	}

	public string GetRandomSkinColorForSkinType(int pSkinType)
	{
		return null;
	}

	public string GetRandomGlassesColorForSkinTypeWeighted(int pSkinType, bool pWeighted)
	{
		return null;
	}

	public void BuildColorPalettes()
	{
	}

	public ArrayList GetSkinColorPalette()
	{
		return null;
	}

	public int GetSkinTypeForColor(string pColor)
	{
		return 0;
	}

	public ArrayList GetSkinTypePalette(int pPalette)
	{
		return null;
	}

	public ArrayList GetEyebrowColorPalette()
	{
		return null;
	}

	public ArrayList GetFacialHairColorPalette()
	{
		return null;
	}

	public ArrayList GetHairColorPalette()
	{
		return null;
	}

	public ArrayList GetRedHairColorPalette()
	{
		return null;
	}

	public ArrayList GetBrownHairColorPalette()
	{
		return null;
	}

	public ArrayList GetDarkBrownHairColorPalette()
	{
		return null;
	}

	public ArrayList GetBlondHairColorPalette()
	{
		return null;
	}

	public ArrayList GetBlackHairColorPalette()
	{
		return null;
	}

	public ArrayList GetGrayHairColorPalette()
	{
		return null;
	}

	public ArrayList GetDyedHairColorPalette()
	{
		return null;
	}

	public ArrayList GetEyeColorPalette()
	{
		return null;
	}

	public ArrayList GetBrownEyeColorPalette()
	{
		return null;
	}

	public ArrayList GetGreenEyeColorPalette()
	{
		return null;
	}

	public ArrayList GetBlueEyeColorPalette()
	{
		return null;
	}

	public ArrayList GetMiscEyeColorPalette()
	{
		return null;
	}

	public string GetRedEyeColor()
	{
		return null;
	}

	public ArrayList GetBlacklistEyeColorPalette()
	{
		return null;
	}

	public ArrayList GetHairAccessoryColorPalette()
	{
		return null;
	}

	public ArrayList GetHeadgearColorPalette()
	{
		return null;
	}

	public ArrayList GetGlassesColorPalette()
	{
		return null;
	}

	public ArrayList GetGlassesBrightColorPalette()
	{
		return null;
	}

	public ArrayList GetGlassesDarkColorPalette()
	{
		return null;
	}

	public ArrayList GetGlassesStandardColorPalette()
	{
		return null;
	}

	public string GetContainerDataFromContainerArray(ArrayList pArray)
	{
		return null;
	}

	public void GetAttributesDataAndHistoryDataFromContainerData(ref string pAttributesData, ref string pHistoryData, string pData)
	{
	}

	public void GetAttributesArrayAndHistoryArrayFromContainerData(ref ArrayList pAttributesArray, ref ArrayList pHistoryArray, string pData)
	{
	}

	public ArrayList GetAttributesArrayFromContainerData(string pData)
	{
		return null;
	}

	public ArrayList GetHistoryArrayFromContainerData(string pData)
	{
		return null;
	}

	public string GetAttributesDataFromContainerData(string pData)
	{
		return null;
	}

	public string GetHistoryDataFromContainerData(string pData)
	{
		return null;
	}

	public ArrayList GetAttributesArrayFromAttributesData(string pData)
	{
		return null;
	}

	public ArrayList GetHistoryArrayFromHistoryData(string pData)
	{
		return null;
	}

	public ArrayList GetComponenetArrayFromHistoryEntryData(string pData)
	{
		return null;
	}

	public ArrayList GetComponenetDetailsArrayFromComponentData(string pData)
	{
		return null;
	}

	public ArrayList GetAttributeDetailsArrayFromAttributeData(string pData)
	{
		return null;
	}

	public string GetAttributesDataFromAttributesArray(ArrayList pArray)
	{
		return null;
	}

	public string GetHistoryDataFromHistoryArray(ArrayList pArray)
	{
		return null;
	}

	public string GetHistoryEntryDataFromComponentArray(ArrayList pArray)
	{
		return null;
	}

	public string GetComponentDataFromComponentDetailsArray(ArrayList pArray)
	{
		return null;
	}

	public string GetAttributeDataFromAttributeDetailsArray(ArrayList pArray)
	{
		return null;
	}

	public string GetClosestMatchToID(string pIDString)
	{
		return null;
	}

	public string GetItemByRollingTheDiceWithItems(ArrayList pItems)
	{
		return null;
	}

	public string GetChildDataByCombiningParent1DataParent2DataGenderAllowDyedHair(string pParent1Data, string pParent2Data, int gender, bool pAllowDyedHair)
	{
		return null;
	}

	public static bool RollTheDiceWithChance(int pChance)
	{
		return false;
	}

	public string GetGenderSwapDataForData(string pData)
	{
		return null;
	}

	public string GetEvolvedDataForData(string pData)
	{
		return null;
	}

	public string GetDevolvedDataForData(string pData)
	{
		return null;
	}

	public string GetGenderSwapIDForID(string pID)
	{
		return null;
	}

	public string GetEvolutionIDForIDFeatureAttributes(string pID, int pFeature, VoltronFaceAttributes pAttributes)
	{
		return null;
	}

	public ArrayList GetEvolutionLinksForIDFeatureAttributes(string pID, int pFeature, VoltronFaceAttributes pAttributes)
	{
		return null;
	}
}
