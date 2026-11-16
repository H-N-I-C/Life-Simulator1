using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Backend.Service
{
	public class ReceiptService : IReceiptService
	{
		private sealed class _003C_003Ec__DisplayClass1_0
		{
			public Action<List<Product>> action;

			public List<Product> products;

			internal void _003CStartSavePurchases_003Eb__0(bool response)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass1_1
		{
			public bool response;

			public _003C_003Ec__DisplayClass1_0 CS_0024_003C_003E8__locals1;

			internal void _003CStartSavePurchases_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public Action<List<TransactionItemModel>> action;

			internal void _003CRestoreAllPurchases_003Eb__0(List<TransactionItemModel> response)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass2_1
		{
			public List<TransactionItemModel> response;

			public _003C_003Ec__DisplayClass2_0 CS_0024_003C_003E8__locals1;

			internal void _003CRestoreAllPurchases_003Eb__1()
			{
			}
		}

		private readonly QueueService _queueService;

		public void StartSavePurchases(string userId, int platform, Action<List<Product>> action)
		{
		}

		public void RestoreAllPurchases(string userId, Action<List<TransactionItemModel>> action)
		{
		}

		public void SaveReceipt(string userId, Product product)
		{
		}

		public void UpdateQueue()
		{
		}
	}
}
