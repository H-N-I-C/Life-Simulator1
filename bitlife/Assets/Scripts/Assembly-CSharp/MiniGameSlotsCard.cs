using UnityEngine;
using UnityEngine.UI;

public class MiniGameSlotsCard : MonoBehaviour
{
	private const float SYMBOL_COUNT = 6f;

	private const float IMAGE_OFFSET = 0.5f;

	private RawImage _image;

	private char _symbol;

	public char Symbol
	{
		get
		{
			return '\0';
		}
		set
		{
		}
	}

	public void SetImageOffset(float offset)
	{
	}

	private void SetSymbolImage(char symbol)
	{
	}

	public float GetImageOffsetForSymbol(char symbol)
	{
		return 0f;
	}

	private float GetImageOffsetForIndex(int i)
	{
		return 0f;
	}
}
