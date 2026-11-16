using System;

namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class ConfidenceParameter
	{
		private ConfidenceParameterValueResolver ValueResolver__BackingField;

		private Type ParameterType__BackingField;

		private ConfidenceParameterValueResolver ValueResolver
		{
			get
			{
				return ValueResolver__BackingField;
			}
			set
			{
				ValueResolver__BackingField = value;
			}
		}

		public Type ParameterType
		{
			get
			{
				return ParameterType__BackingField;
			}
			private set
			{
				ParameterType__BackingField = value;
			}
		}

		public object GetValueForString(string parameterString)
		{
			return null;
		}

		public static ConfidenceParameter CreateInstance(string typeValue)
		{
			return null;
		}
	}
}
