using System;
using UnityEngine;

public class OnDisableDispatcher : MonoBehaviour
{
	private Action<Transform> m_OnDisableHandler;

	public event Action<Transform> OnDisableHandler
	{
		add
		{
		}
		remove
		{
		}
	}

	private void OnDisable()
	{
	}
}
