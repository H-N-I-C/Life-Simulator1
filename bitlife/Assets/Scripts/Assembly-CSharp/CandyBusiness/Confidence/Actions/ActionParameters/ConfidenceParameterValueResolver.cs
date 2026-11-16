namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public abstract class ConfidenceParameterValueResolver
	{
		public object Resolve(string parameterString)
		{
			return null;
		}

		protected abstract object ResolveInternalObject(string parameterString);

		protected abstract object DefaultValue();
	}
	public abstract class ConfidenceParameterValueResolver<T> : ConfidenceParameterValueResolver
	{
		protected sealed override object ResolveInternalObject(string parameterString)
		{
			return null;
		}

		protected sealed override object DefaultValue()
		{
			return null;
		}

		protected abstract T ResolveInternal(string parameterString);
	}
}
