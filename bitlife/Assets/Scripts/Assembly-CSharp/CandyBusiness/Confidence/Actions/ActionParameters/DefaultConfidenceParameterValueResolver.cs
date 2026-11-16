using System;

namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class DefaultConfidenceParameterValueResolver : ConfidenceParameterValueResolver
	{
		private readonly Type _type;

		public DefaultConfidenceParameterValueResolver(Type type)
		{
		}

		protected override object ResolveInternalObject(string parameterString)
		{
			return null;
		}

		protected override object DefaultValue()
		{
			return null;
		}
	}
}
