using BitLife.UI.CandyObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyInformation : UIDarkly
	{
		private const string COLOR_WHITE = "FFFFFF";

		private const string COLOR_YELLOW_CANARY = "FCF672";

		private const string COLOR_BLUE_ICE = "61D6FD";

		private const string COLOR_ORANGE_PEACH = "FFAD51";

		private const string BULLET_NAME_DEFAULT = "a16_darkBullet";

		private const float MAX_TEXT_WIDTH = 750f;

		private const float GAP_SIZE_SMALL = 33f;

		private const float ENTRY_TOTAL_MARGIN_VERTICAL = 60f;

		private const float EVEN_ENTRY_BG_COLOR_ALPHA = 0.28f;

		private readonly Vector2 EMOJI_SIZE_SMALL;

		private CandyImage border;

		private GameObject banner;

		private CandyText bannerText;

		private CandyImage titleEmoji;

		private CandyImage titleEmojiBorder;

		private CandyImage titleFX;

		private GameObject noFxTitleSpace;

		private CandyText details;

		private RectTransform entryTemplate;

		private CandyImage entryTemplateEmoji;

		private CandyText entryTemplateText;

		private RectTransform entryTemplateCmdRect;

		private CandyImage entryTemplateCmdImage;

		private RectTransform contentParent;

		private CandyButton actionButton;

		private CandyImage actionButtonEmoji;

		private HorizontalLayoutGroup actionButtonTitleGroup;

		private GameObject bottomSection;

		private TextMeshProUGUI actionButtonText;

		private Sprite customBackgroundImage;

		private int infoType;

		public static string LiveDarklyID;

		public static string OriginID;

		public void Setup(int infoType)
		{
		}

		private void SetupBulletPoint(string bulletColor, string emojiName, ref Sprite bulletImage, ref string currentEmojiName, ref float runningHeight, string text)
		{
		}

		private void ActionButtonPress()
		{
		}

		private void Exit()
		{
		}
	}
}
