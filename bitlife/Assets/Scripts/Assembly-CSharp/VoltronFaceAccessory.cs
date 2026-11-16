using System.Collections;
using UnityEngine;

public class VoltronFaceAccessory
{
	public int mType;

	public int mStyle;

	public VoltronFaceAccessory(int pType, int pStyle)
	{
	}

	private string GetSegment()
	{
		return null;
	}

	public string GetFilename()
	{
		return null;
	}

	public string GetFolder()
	{
		return null;
	}

	private bool CoversOverEyeAccessories()
	{
		return false;
	}

	public Vector2 GetPositionForGenderStage(int pGender, int pStage)
	{
		return default;
	}

	public Vector2 GetScaleForGenderStage(int pGender, int pStage)
	{
		return default;
	}

	public int GetFeatureToLayerAboveForGenderStage(int pGender, int pStage)
	{
		return 0;
	}

	public ArrayList GetSubmissiveFeaturesForGenderStage(int pGender, int pStage)
	{
		return null;
	}

	public Sprite GetSprite()
	{
		return null;
	}
}
