using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Backend;
using UnityEngine.Purchasing;

public class SROptions
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CAddItemToStorage_003Eb__1_0_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			private TaskAwaiter<(bool, string)> _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Task> _003C_003E9__1_0;

		public static Action<List<TransactionItemModel>> _003C_003E9__2_0;

		public static Action<List<Product>> _003C_003E9__7_0;

		public static Func<string, string, string> _003C_003E9__14_0;

		internal Task _003CAddItemToStorage_003Eb__1_0()
		{
			return null;
		}

		internal void _003CRestoreWebShopItems_003Eb__2_0(List<TransactionItemModel> result)
		{
		}

		internal void _003CSavePurchases_003Eb__7_0(List<Product> result)
		{
		}

		internal string _003CShowBreadCrumbs_003Eb__14_0(string i, string j)
		{
			return null;
		}
	}

	public void UserCountry()
	{
	}

	public void AddItemToStorage()
	{
	}

	public void RestoreWebShopItems()
	{
	}

	public void SetWebshopUS()
	{
	}

	public void SetWebshopOthers()
	{
	}

	public void SetWebshopNoWebShop()
	{
	}

	public void RolloutStatus()
	{
	}

	public void SavePurchases()
	{
	}

	public void ShowAvailablePurchases()
	{
	}

	public void ForceDisconnectGuest()
	{
	}

	public void ForceDisconnectBreak()
	{
	}

	public void ForceDisconnectAccount()
	{
	}

	public void Space1()
	{
	}

	public void Space2()
	{
	}

	public void ShowBreadCrumbs()
	{
	}

	public void MarketPlaceList1()
	{
	}

	public void MarketPlaceList2()
	{
	}

	public void MarketPlaceListSHORT()
	{
	}

	public void MarketPlaceDefault()
	{
	}

	public void MarketPlaceListOutput()
	{
	}
}
