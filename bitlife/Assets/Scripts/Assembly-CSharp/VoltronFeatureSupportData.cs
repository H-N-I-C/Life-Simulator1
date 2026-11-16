using System.Collections;

public class VoltronFeatureSupportData
{
	public bool mCanEvolve;

	public bool mOptional;

	public bool mColorizable;

	public bool mShouldDarkenColor;

	public bool mExposeOnlyOneStyle;

	public int mFeature;

	public int mDefaultStyle;

	public int mEditableRule;

	public int mLayerPosition;

	public string mDefaultColor;

	public string mSimpleName;

	public string mStyleName;

	public string mFolder;

	public string mFilenameSegment;

	public ArrayList mAssociatedFeatures;

	public ArrayList mSubmissiveFeatures;

	public int mColorPaletteID;

	public VoltronFeatureSupportData(int pFeature)
	{
	}

	public string GetNameForStyleStage(int pStyle, int pStage)
	{
		return null;
	}

	public int GetNumberOfMoodVersions(int pGender, int pStage)
	{
		return 0;
	}
}
