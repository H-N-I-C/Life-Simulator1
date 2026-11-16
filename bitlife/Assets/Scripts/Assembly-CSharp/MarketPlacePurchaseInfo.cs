using System;
using System.Collections.Generic;
using Backend.Code.Models.Catalog;
using Game;

public class MarketPlacePurchaseInfo
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<MarketPlacePurchaseInfo> _003C_003E9__21_0;

		internal bool _003CGetPurchasedInfo_003Eb__21_0(MarketPlacePurchaseInfo product)
		{
			return false;
		}
	}

	private IAP_TYPE IapType__BackingField;

	private Price WebShopPrice__BackingField;

	private float? PriceAmount__BackingField;

	private List<string> Ids__BackingField;

	public IAP_TYPE IapType
	{
		get
		{
			return IapType__BackingField;
		}
		private set
		{
			IapType__BackingField = value;
		}
	}

	public Price WebShopPrice
	{
		get
		{
			return WebShopPrice__BackingField;
		}
		private set
		{
			WebShopPrice__BackingField = value;
		}
	}

	public float? PriceAmount
	{
		get
		{
			return PriceAmount__BackingField;
		}
		private set
		{
			PriceAmount__BackingField = value;
		}
	}

	public List<string> Ids
	{
		get
		{
			return Ids__BackingField;
		}
		private set
		{
			Ids__BackingField = value;
		}
	}

	private static MonetizationVars Monetization => null;

	public MarketPlacePurchaseInfo(IAP_TYPE iapType)
	{
	}

	public MarketPlacePurchaseInfo Add(string id)
	{
		return null;
	}

	public bool HasId(string id)
	{
		return false;
	}

	public static List<MarketPlacePurchaseInfo> GetPurchasedInfo()
	{
		return null;
	}
}
