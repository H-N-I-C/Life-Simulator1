using System.Collections.Generic;

namespace Backend
{
	public class PutAllReceiptsRequest
	{
		public string USER_ID;

		public List<TransactionItemModel> Receipts;
	}
}
