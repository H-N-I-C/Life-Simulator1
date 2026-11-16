namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class SByteConfidenceParameterValueResolver : ConfidenceParameterValueResolver<sbyte>
	{
		protected override sbyte ResolveInternal(string parameterString)
		{
			return 0;
		}
	}
}
