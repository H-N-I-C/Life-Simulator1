using UnityEngine;

public class MiniGamePowerBar : MonoBehaviour
{
	public delegate void SetPowerDelegate(float power);

	private bool TouchDown__BackingField;

	private bool TouchMove__BackingField;

	private bool _initialized;

	private SetPowerDelegate _setPowerDelegate;

	private GameObject _foregroundObject;

	private GameObject _backgroundObject;

	private GameObject _pseudoForegroundObject;

	private GameObject _frameObject;

	private float _verticalBuffer;

	private bool _enabled;

	private float _lastRecordedPower;

	public bool TouchDown
	{
		get
		{
			return TouchDown__BackingField;
		}
		set
		{
			TouchDown__BackingField = value;
		}
	}

	public bool TouchMove
	{
		get
		{
			return TouchMove__BackingField;
		}
		set
		{
			TouchMove__BackingField = value;
		}
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
	}

	public void InitWithParameters(Vector2 size, float verticalBuffer, SetPowerDelegate setPowerDelegate)
	{
	}

	public void SetEnabled(bool enabled)
	{
	}

	private Vector2 MouseToPosition(Vector3 touchLocation)
	{
		return default;
	}

	private void SetPowerBarValue(float value, bool animated = false)
	{
	}

	private void SetPowerDisplayToValue(float value, bool animated = false)
	{
	}

	private void MakePseudoPowerVisible()
	{
	}

	private bool LocationInVerticalZone(Vector3 touchLocation)
	{
		return false;
	}

	private void HandleTouchDown(Vector3 touchLocation)
	{
	}

	private void HandleTouchMoved(Vector3 touchLocation)
	{
	}

	private float LocationToPower(Vector3 touchLocation)
	{
		return 0f;
	}

	private void HandleTouchUp(Vector3 pTouchLocation)
	{
	}
}
