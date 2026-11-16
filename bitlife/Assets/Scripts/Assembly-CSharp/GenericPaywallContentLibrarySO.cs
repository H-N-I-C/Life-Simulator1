using System;
using UnityEngine;

public class GenericPaywallContentLibrarySO : ScriptableObject
{
	[Serializable]
	public class PaywallContentReference
	{
		public IAP_TYPE IAPType;

		public GenericPaywallContentSO PaywallContent;
	}

	public PaywallContentReference[] PaywallContents;
}
