using System;
using UnityEngine.Purchasing;

public class ProductArgs : EventArgs
{
	private bool Purchase__BackingField;

	private Product AProduct__BackingField;

	public bool Purchase
	{
		get
		{
			return Purchase__BackingField;
		}
		set
		{
			Purchase__BackingField = value;
		}
	}

	public Product AProduct
	{
		get
		{
			return AProduct__BackingField;
		}
		set
		{
			AProduct__BackingField = value;
		}
	}

	public ProductArgs(Product aProduct = null, bool purchase = false)
	{
	}
}
