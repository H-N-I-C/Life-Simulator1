using UnityEngine;

public class ShuttleObstacle : MonoBehaviour
{
	public int obstacleID;

	public int obstacleType;

	public float asteroidRotationDuration;

	public bool asteroidRotationClockwiseDirection;

	public MiniGameShuttle miniGameObject;

	public float scale;

	public float bobPixels;

	public float bobLimit;

	public float vertOrigin;

	private float currentTime;

	private float asteroidStartRotation;

	private float asteroidEndRotation;

	private bool testDirection;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void UpdateAsteroidRotation()
	{
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
	}
}
