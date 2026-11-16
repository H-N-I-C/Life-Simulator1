using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Backend.Service
{
	public class QueueService
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Predicate<ProductModel> _003C_003E9__6_0;

			internal bool _003CLoadQueue_003Eb__6_0(ProductModel m)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass4_0
		{
			public bool result;

			public QueueService _003C_003E4__this;

			internal void _003CSaveToServer_003Eb__1()
			{
			}
		}

		private List<ProductModel> _queue;

		private int _saving;

		public void AddItem(string userId, Product product)
		{
		}

		private void SaveToServer(ProductModel model)
		{
		}

		public void UpdateQueue()
		{
		}

		private void LoadQueue()
		{
		}

		private void SaveQueue()
		{
		}

		private void _003CSaveToServer_003Eb__4_0(bool result)
		{
		}
	}
}
