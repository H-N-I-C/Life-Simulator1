using System;

namespace OPS.Serialization.Attributes
{
	public class SerializeAbleFieldAttribute : Attribute
	{
		private int index;

		public int Index => 0;

		public SerializeAbleFieldAttribute(int _Index)
		{
		}
	}
}
