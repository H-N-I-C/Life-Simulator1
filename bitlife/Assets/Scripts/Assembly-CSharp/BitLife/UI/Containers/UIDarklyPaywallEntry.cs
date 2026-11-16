using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallEntry : MonoBehaviour
	{
		public RectTransform rectTransform;

		public CandyImage entryTemplateBackground;

		public CandyText entryTemplateText;

		public CandyImage entryTemplateEmoji;

		public RectTransform entryTitleContainer;

		public float Height => 0f;

		public void RebuildUI()
		{
		}
	}
}
