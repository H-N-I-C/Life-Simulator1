using UnityEngine;

namespace QRCodeShareMain
{
	public class QRImageProperties
	{
		private int _width;

		private int _height;

		private int _topMargin;

		private int _bottomMargin;

		private int _leftMargin;

		private int _rightMargin;

		private Color32 _foregroundColor;

		private Color32 _backgroundColor;

		private Color32 _paddingColor;

		public int Width
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int Height
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int TopMargin
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int BottomMargin
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int LeftMargin
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int RightMargin
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int OuterWidth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int OuterHeight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public Color32 ForegroundColor
		{
			get
			{
				return default;
			}
			set
			{
			}
		}

		public Color32 BackgroundColor
		{
			get
			{
				return default;
			}
			set
			{
			}
		}

		public Color32 PaddingColor
		{
			get
			{
				return default;
			}
			set
			{
			}
		}

		public QRImageProperties()
		{
		}

		public QRImageProperties(int width, int height, int topMargin, int bottomMargin, int leftMargin, int rightMargin, Color32 foregroundColor, Color32 backgroundColor, Color32 paddingColor)
		{
		}

		public QRImageProperties(int width, int height, int margin)
		{
		}

		public void SetUniformMargin(int margin)
		{
		}

		public void SetAllMargins(int topMargin, int bottomMargin, int leftMargin, int rightMargin)
		{
		}

		public void SetAllColors(Color32 foregroundColor, Color32 backgroundColor, Color32 paddingColor)
		{
		}

		public Vector2Int GetQRCodeCenter()
		{
			return default;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
