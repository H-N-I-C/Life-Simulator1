using System;

namespace OPS.Serialization.Attributes
{
	public class ClassInheritanceAttribute : Attribute
	{
		private Type inheritanceType;

		private int index;

		public Type InheritanceType => null;

		public int Index => 0;

		public ClassInheritanceAttribute(Type _InheritanceType, int _Index)
		{
		}
	}
}
