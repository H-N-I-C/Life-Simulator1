using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class iOSGestures : MonoBehaviour
{
	public Text m_DebugOutputLabel;

	private UAP_AccessibilityManager m_UAP;

	[PreserveSig]
	private static extern void InitGestureRecognition();

	[PreserveSig]
	private static extern void RemoveGestureRecognition();

	public void StartRecognition()
	{
	}

	public void StopRecognition()
	{
	}

	public void SetUAP(UAP_AccessibilityManager UAP_Instance)
	{
	}

	private void OnSwipeLeftCallback(string fingerCount)
	{
	}

	private void OnSwipeRightCallback(string fingerCount)
	{
	}

	private void OnSwipeUpCallback(string fingerCount)
	{
	}

	private void OnSwipeDownCallback(string fingerCount)
	{
	}
}
