using System.Collections.Generic;

namespace Game
{
	public class DataModelProduct
	{
		private List<string> ProductNames__BackingField;

		private string Category__BackingField;

		private int TypicalModelGender__BackingField;

		public List<string> ProductNames
		{
			get
			{
				return ProductNames__BackingField;
			}
			set
			{
				ProductNames__BackingField = value;
			}
		}

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

		public int TypicalModelGender
		{
			get
			{
				return TypicalModelGender__BackingField;
			}
			set
			{
				TypicalModelGender__BackingField = value;
			}
		}
	}
}
