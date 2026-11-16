using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace Tracking
{
	public class UnityConnector : MonoBehaviour
	{
		private readonly ConcurrentQueue<Action> _actions;

		private void Start()
		{
		}

		private void Check()
		{
		}

		public void AddAction(Action action)
		{
		}
	}
}
