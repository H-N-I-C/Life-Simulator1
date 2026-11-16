using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyAppearanceVoltron : MonoBehaviour
	{
		public enum FACE
		{
			PREVIOUS = 0,
			NEXT = 1
		}

		public int currentFaceFeature;

		private bool initialized;

		private float height;

		private float width;

		private bool customPerson;

		private int currentStyleArrayIndex;

		private string defaultData;

		private VoltronFaceContainerView activeFaceView;

		private VoltronFaceContainerView oldFaceView;

		private ArrayList stylesArray;

		private bool isVampire;

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void InitWithParameters(float pWidth, float pHeight, string pData, bool pCustomPerson, bool pIsVampire)
		{
		}

		public string GetData()
		{
			return null;
		}

		public int GetFeatureForFeatureIndex(int featureIndex)
		{
			return 0;
		}

		public void ChangeToColorIndexForFeature(int index, int feature)
		{
		}

		public void ResetFaceWithData(string data)
		{
		}

		public ArrayList GetFeatures()
		{
			return null;
		}

		private bool ValidToShowFeature(int feature)
		{
			return false;
		}

		public bool ColorizableForFeature(int feature)
		{
			return false;
		}

		public void ShowHijabWithLayerUpdate(bool show, bool layerUpdate)
		{
		}

		public bool ShouldShowStylePickingForFeature(int feature)
		{
			return false;
		}

		public void Randomize()
		{
		}

		public List<Color> GetPaletteColorsForCurrentFeature()
		{
			return null;
		}

		private ArrayList GetPaletteColorsForFeature(int feature)
		{
			return null;
		}

		public void DisplayStylesForFeatureStyleIndex(int feature, int index)
		{
		}

		public void SyncAttributes()
		{
		}

		public int GetColorIndexForCurrentFeature()
		{
			return 0;
		}

		public int GetStyleForCurrentFeature()
		{
			return 0;
		}

		public void SwitchToStylesForFeature(int feature)
		{
		}

		public int GetStyleForAccessoryType(int type)
		{
			return 0;
		}

		public void PushDirection(int direction)
		{
		}

		private void KillOldFace()
		{
		}
	}
}
