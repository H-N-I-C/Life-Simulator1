using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Backend
{
	public interface IReceiptService
	{
		void StartSavePurchases(string userId, int platform, Action<List<Product>> action);

		void RestoreAllPurchases(string userId, Action<List<TransactionItemModel>> action);

		void SaveReceipt(string userId, Product product);

		void UpdateQueue();
	}
}
