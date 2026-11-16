namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class DecimalConfidenceParameterValueResolver : ConfidenceParameterValueResolver<decimal>
	{
		protected override decimal ResolveInternal(string parameterString)
		{
			return 0m;
		}
	}
}
