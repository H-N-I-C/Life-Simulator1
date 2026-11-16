using UnityEngine;

public class MiniGameHorseRacingProgressMeter : MonoBehaviour
{
	private bool mInitialized;

	private GameObject mColorBar;

	private GameObject mHorse;

	public bool mActive;

	public int mIndex;

	private float mDuration;

	private float mCurrentTime;

	private Vector2 mStartSize;

	private Vector2 mEndSize;

	private MiniGameHorseRacing mHorseRacingGameScript;

	private int mGrowCounter;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(int pIndex)
	{
	}

	private void UpdateGrow()
	{
	}

	public void GrowTo(float pAmount, float pDuration, MiniGameHorseRacing pMiniGame)
	{
	}

	public void HorseRocker()
	{
	}

	public void HaltHorseRocking()
	{
	}

	public void DropHorse()
	{
	}
}
