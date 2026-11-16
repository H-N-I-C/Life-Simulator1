using System.Collections.Generic;

namespace Backend.Code.Models.Catalog
{
	public class ProductCatalogWebShop
	{
		private List<ProductGroup> ProductGroups__BackingField;

		public List<ProductGroup> ProductGroups
		{
			get
			{
				return ProductGroups__BackingField;
			}
			set
			{
				ProductGroups__BackingField = value;
			}
		}
	}
}
