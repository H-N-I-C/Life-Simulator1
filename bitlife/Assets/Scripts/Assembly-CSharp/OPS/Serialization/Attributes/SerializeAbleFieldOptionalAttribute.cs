using System;

namespace OPS.Serialization.Attributes
{
	public class SerializeAbleFieldOptionalAttribute : Attribute
	{
		private int index;

		public int Index => 0;

		public SerializeAbleFieldOptionalAttribute(int _Index)
		{
		}
	}
}
