using System.Collections.Generic;

namespace Backend.Code.Models.Catalog
{
	public class ProductGroup
	{
		private string Category__BackingField;

		private List<ProductItem> Products__BackingField;

		public string Category
		{
			get
			{
				return Category__BackingField;
			}
			set
			{
				Category__BackingField = value;
			}
		}

		public List<ProductItem> Products
		{
			get
			{
				return Products__BackingField;
			}
			set
			{
				Products__BackingField = value;
			}
		}
	}
}
