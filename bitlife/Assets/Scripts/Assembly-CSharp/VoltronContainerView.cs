using System.Collections;
using UnityEngine;

public class VoltronContainerView : MonoBehaviour
{
	public ArrayList mLayers;

	public VoltronContainer mContainer;

	public bool mInitialized;

	public Vector2 mSize;

	private float mDiv;

	private byte mAlphaLimit;

	private byte mFull;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void BaseInitialize(Vector2 pSize)
	{
	}

	public void InitWithDefaultSettingsAndSize(Vector2 pSize)
	{
	}

	public void InitWithParameters(Vector2 pSize, string pData)
	{
	}

	public virtual void BuildDefaultSettingsContainer()
	{
	}

	public virtual float GetAlphaForFeature(int pFeature)
	{
		return 0f;
	}

	public void FadeLayers(float lDestinationValue, float pDuration)
	{
	}

	public void CreateLayers()
	{
	}

	public void InitializeVariables()
	{
	}

	public void ResetVariables()
	{
	}

	public virtual void BuildContainerWithData(string pData)
	{
	}

	public void HideFeature(int pFeature)
	{
	}

	public void CreateLayerForAccessory(VoltronFaceAccessory pAccessory)
	{
	}

	public void CreateLayerForDisguise(VoltronFaceDisguise pDisguise)
	{
	}

	private void SyncSortOrder()
	{
	}

	public void UnloadResources()
	{
	}

	public void CreateLayerForComponent(VoltronComponent pComponent)
	{
	}

	public GameObject LayerForFeature(int pFeature)
	{
		return null;
	}

	public GameObject LayerForAccessory(int pAccessory)
	{
		return null;
	}

	public GameObject LayerForDisguiseType(int pType)
	{
		return null;
	}

	public Color DarkerColorForColor(Color pColor)
	{
		return default;
	}

	public void SetColorForFeature(string pColor, int pFeature)
	{
	}

	public void SetStyleForFeature(int pStyle, int pFeature)
	{
	}

	public void SetAccessoryStyle(int pType, int pStyle)
	{
	}

	private void SetColorForDisguise(VoltronFaceDisguise pDisguise)
	{
	}

	public void SetDisguiseTypeStyleColor(int pType, int pStyle, string pColor)
	{
	}

	public void UpdateLayers()
	{
	}

	public virtual string GetCurrentFolderForFeature(int pFeature)
	{
		return null;
	}

	public Texture2D GetCompositedImage(bool centered = true)
	{
		return null;
	}

	public void CombineTextures(Color32[] pData1, Color32[] pData2, Color32 pColor, bool pChangeColor)
	{
	}

	private void CombineTextures(Color32[] pData1, Color32[] pData2, Vector2 pData2Offset, Vector2 pData2Size)
	{
	}
}
