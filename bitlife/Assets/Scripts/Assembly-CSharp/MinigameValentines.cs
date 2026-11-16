using BitLife.UI.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing.QrCode;

public class MinigameValentines : MonoBehaviour, IguiObject
{
	private RectTransform rect;

	private Image qrCodeImage;

	private TextMeshProUGUI playerIdText;

	private Texture2D _qrCodeTexture;

	private JsonExportHeroActivity _activity;

	private Scenario _scenario;

	private bool _ended;

	private const int QrMargin = 1;

	private const int QrSize = 500;

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	private static Texture2D CreateQrCodeImage(string content, QrCodeEncodingOptions options)
	{
		return null;
	}

	public void SetHeightConstraint()
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	private void Update()
	{
	}

	private void ShowQrCode()
	{
	}

	private void ExitWithError()
	{
	}
}
