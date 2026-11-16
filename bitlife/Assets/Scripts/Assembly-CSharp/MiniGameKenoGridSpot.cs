using UnityEngine;

public class MiniGameKenoGridSpot : MonoBehaviour
{
	private bool _initialized;

	private int GridIndex__BackingField;

	private Vector3 Center__BackingField;

	private bool Selected__BackingField;

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

	public bool Selected
	{
		get
		{
			return Selected__BackingField;
		}
		set
		{
			Selected__BackingField = value;
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

	public void Select(bool includeSound = true)
	{
	}

	public void Deselect(bool includeSound = true)
	{
	}
}
