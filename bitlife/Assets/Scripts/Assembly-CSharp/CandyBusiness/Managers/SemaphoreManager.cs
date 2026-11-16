using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

namespace CandyBusiness.Managers
{
	public class SemaphoreManager
	{
		private sealed class _003C_003Ec__DisplayClass14_0
		{
			public int iapType;

			internal bool _003CGetPlatformSalePercentage_003Eb__0(KeyValuePair<string, object> x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass9_0
		{
			public int IAP;

			internal bool _003CIsIapOnSale_003Eb__0(KeyValuePair<string, object> x)
			{
				return false;
			}
		}

		private sealed class _003CGetRequest_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public string uri;

			public string _bundleID;

			private UnityWebRequest _003Cwww_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CGetRequest_003Ed__5(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			void IEnumerator.Reset()
			{
			}
		}

		public static Dictionary<string, object> LiveDictionary;

		public static void InitializeDictionary()
		{
		}

		public static void CompareDeviceTimeToServerTime(JSONNode serverTimeString)
		{
		}

		public static bool IsHonestAboutDeviceTime()
		{
			return false;
		}

		public static void RunSemaphore(string _appUrl, string _bundleID, string _version, MonoBehaviour _obj)
		{
		}

		private static IEnumerator GetRequest(string uri, string _bundleID)
		{
			return null;
		}

		private static void ParseLiveDictionary(string respose)
		{
		}

		private static string CleanSemaphoreValue(object o)
		{
			return null;
		}

		public static bool IsSaleActive()
		{
			return false;
		}

		public static bool IsIapOnSale(int action)
		{
			return false;
		}

		public static int GetIAPTypeFromSelectionAction(int action)
		{
			return 0;
		}

		public static DateTime GetSaleEndTime()
		{
			return default;
		}

		public static int GetSalePercentage(int iapType, bool isWebShop)
		{
			return 0;
		}

		private static int GetWebShopSalePercentage(int iapType)
		{
			return 0;
		}

		private static int GetPlatformSalePercentage(int iapType)
		{
			return 0;
		}
	}
}
