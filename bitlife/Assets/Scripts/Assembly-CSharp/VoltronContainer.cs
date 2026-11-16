using System.Collections;
using UnityEngine;

public class VoltronContainer
{
	public string mImageCacheID;

	public ArrayList mComponents;

	public VoltronFaceAttributes mAttributes;

	public ArrayList mHistory;

	public ArrayList mStartingFeatureColors;

	public VoltronContainer()
	{
	}

	public VoltronContainer(string pData)
	{
	}

	public void InitWithDefaultSettings()
	{
	}

	public void OverrideImageCacheIDWithID(string pImageCacheID)
	{
	}

	public void initializeImageData()
	{
	}

	public void initializeHistory()
	{
	}

	public void ClearHistory()
	{
	}

	public void UpdateHistory()
	{
	}

	public void CreateContainer()
	{
	}

	public void CreateDefaultContainer()
	{
	}

	public virtual void CreateComponentForFeature(int pFeature)
	{
	}

	private VoltronComponent GetDefaultComponentForFeatureStageGender(int pFeature, int pStage, int pGender)
	{
		return null;
	}

	private void CreateDefaultComponentForFeature(int pFeature)
	{
	}

	public void SetStageWithNoEvolution(int pStage)
	{
	}

	public void SetStage(int pStage)
	{
	}

	public void SetAge(int pAge)
	{
	}

	public bool HistoryExistsForStage(int pStage)
	{
		return false;
	}

	public int GetAccessoryStyleForType(int pType)
	{
		return 0;
	}

	public void SetAccessoryStyle(int pType, int pStyle)
	{
	}

	public int GetDisguiseStyleForType(int pType)
	{
		return 0;
	}

	public void SetDisguiseTypeStyleColor(int pType, int pStyle, string pColor)
	{
	}

	public void Evolve()
	{
	}

	private void DevolveWithWipe(bool pWipe)
	{
	}

	public void ExtrapolateDevolutionButKeepCurrentStage(bool pKeepCurrentStage)
	{
	}

	private string GetDataWithoutUpdatingHistory()
	{
		return null;
	}

	public string GetData()
	{
		return null;
	}

	private string GetCurrentStageData()
	{
		return null;
	}

	public void SetData(string pData)
	{
	}

	private void VerifyColors()
	{
	}

	private void VerifyHistory()
	{
	}

	private void VerifyIDs()
	{
	}

	public VoltronComponent GetComponentForFeature(int pFeature)
	{
		return null;
	}

	public int GetNumberOfEditableFeatures()
	{
		return 0;
	}

	public int GetStyleForFeature(int pFeature)
	{
		return 0;
	}

	public int GetNumberOfExposedStylesForFeature(int pFeature)
	{
		return 0;
	}

	public int GetStage()
	{
		return 0;
	}

	public void SetStyleForFeature(int pStyle, int pFeature)
	{
	}

	private void SyncStyleForFeature(int pStyle, int pFeature)
	{
	}

	private void HideSubmissiveFeaturesBasedOnStyleStateNoneForSubmissiveFeatures(bool pStyleStateNone, ArrayList pSubmissiveFeatures)
	{
	}

	public void SyncStyles()
	{
	}

	public string GetLabelForEditableComponentAtIndex(int pIndex)
	{
		return null;
	}

	public string GetLabelForStyleForFeature(int pStyle, int pFeature)
	{
		return null;
	}

	public int GetEditableIndexForFeature(int pFeature)
	{
		return 0;
	}

	public int GetFeatureForEditableComponentAtIndex(int pIndex)
	{
		return 0;
	}

	public string GetColorForFeature(int pFeature)
	{
		return null;
	}

	public void SetColorForFeature(string pColor, int pFeature)
	{
	}

	public string GetFilenameIDForFeature(int pFeature)
	{
		return null;
	}

	public void UpdateImageWithView(GameObject pView, bool centered = true)
	{
	}
}
