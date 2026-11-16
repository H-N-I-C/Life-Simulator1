using System;
using UnityEngine;

public class GenericPaywallPrefabLibrarySO : ScriptableObject
{
	[Serializable]
	public class GenericPaywallPrefabEntry
	{
		public IAP_TYPE IapType;

		public GameObject Prefab;
	}

	public GenericPaywallPrefabEntry[] Prefabs;

	public GameObject DefaultPrefab;
}
