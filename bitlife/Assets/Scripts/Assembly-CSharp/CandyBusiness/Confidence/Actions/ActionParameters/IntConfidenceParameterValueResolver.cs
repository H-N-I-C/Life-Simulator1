namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class IntConfidenceParameterValueResolver : ConfidenceParameterValueResolver<int>
	{
		protected override int ResolveInternal(string parameterString)
		{
			return 0;
		}
	}
}
