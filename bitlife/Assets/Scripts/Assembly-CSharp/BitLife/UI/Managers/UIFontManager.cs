using TMPro;
using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIFontManager : MonoBehaviour
	{
		public static UIFontManager Inst;

		private const string PREFIX = "Fonts & Materials/";

		private string[] fontNames;

		public TMP_FontAsset GetFont(int fontIndex)
		{
			return null;
		}

		public int GetFontIndex(string name)
		{
			return 0;
		}

		public void Awake()
		{
		}
	}
}
