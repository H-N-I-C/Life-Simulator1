using System;

namespace Genesis.SerializableLists
{
	public class Param
	{
		public enum ParamType
		{
			String = 0,
			Int = 1,
			Boolean = 2
		}

		public ParamType Type;

		public string Name;

		public string Value;

		public Type GetParamType()
		{
			return null;
		}

		public object GetParamValue()
		{
			return null;
		}
	}
}
