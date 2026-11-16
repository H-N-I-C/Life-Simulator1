namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class ShortConfidenceParameterValueResolver : ConfidenceParameterValueResolver<short>
	{
		protected override short ResolveInternal(string parameterString)
		{
			return 0;
		}
	}
}
