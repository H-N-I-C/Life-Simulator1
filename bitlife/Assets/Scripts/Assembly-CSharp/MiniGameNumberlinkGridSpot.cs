using UnityEngine;

public class MiniGameNumberlinkGridSpot : MonoBehaviour
{
	private bool _initialized;

	private int GridIndex__BackingField;

	private int ID__BackingField;

	private NUMBERLINK_COLOR_TYPE ColorType__BackingField;

	private Vector3 Center__BackingField;

	private GameObject _lineObject;

	private GameObject _homeBaseDotObject;

	private GameObject _endCapObject;

	private GameObject _IDObject;

	private NUMBERLINK_LINE_TYPE _lineType;

	private Color _redColor;

	private Color _greenColor;

	private Color _blueColor;

	private Color _yellowColor;

	public int GridIndex
	{
		get
		{
			return GridIndex__BackingField;
		}
		set
		{
			GridIndex__BackingField = value;
		}
	}

	public int ID
	{
		get
		{
			return ID__BackingField;
		}
		set
		{
			ID__BackingField = value;
		}
	}

	public NUMBERLINK_COLOR_TYPE ColorType
	{
		get
		{
			return ColorType__BackingField;
		}
		set
		{
			ColorType__BackingField = value;
		}
	}

	public Vector3 Center
	{
		get
		{
			return Center__BackingField;
		}
		set
		{
			Center__BackingField = value;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(int gridIndex)
	{
	}

	public void ClearAll()
	{
	}

	public bool IsEmpty()
	{
		return false;
	}

	public void AddHomeBaseDotWithColor(NUMBERLINK_COLOR_TYPE colorType, int id)
	{
	}

	public void AddLineEndCap(NUMBERLINK_COLOR_TYPE colorType)
	{
	}

	public void RemoveLineEndCap()
	{
	}

	public bool HasHomeBaseDot()
	{
		return false;
	}

	public void RemoveLine()
	{
	}

	public void ShowLineWithType(NUMBERLINK_LINE_TYPE type, NUMBERLINK_COLOR_TYPE colorType)
	{
	}

	private Color ColorForType(NUMBERLINK_COLOR_TYPE colorType)
	{
		return default;
	}
}
