using System;
using System.Collections.Generic;
using UnityEngine;

namespace Backend.GoogleSdk
{
	public class GoogleServiceLifeCycle : MonoBehaviour
	{
		private readonly List<Action> _actions;

		private readonly object _lockObject;

		private Action _executeItem;

		public Action TimeAction;

		public Action OnQuitAction;

		private void Awake()
		{
		}

		private void onDeepLinkActivated(string url)
		{
		}

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		private void FixedUpdate()
		{
		}

		public void AddAction(Action item)
		{
		}

		private void CheckStats()
		{
		}

		private void OnApplicationQuit()
		{
		}
	}
}
