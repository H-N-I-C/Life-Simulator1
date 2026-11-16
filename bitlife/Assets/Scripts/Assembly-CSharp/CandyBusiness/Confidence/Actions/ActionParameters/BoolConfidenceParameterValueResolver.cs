namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class BoolConfidenceParameterValueResolver : ConfidenceParameterValueResolver<bool>
	{
		protected override bool ResolveInternal(string parameterString)
		{
			return false;
		}
	}
}
