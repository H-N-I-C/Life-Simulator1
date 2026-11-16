namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class ULongConfidenceParameterValueResolver : ConfidenceParameterValueResolver<ulong>
	{
		protected override ulong ResolveInternal(string parameterString)
		{
			return 0uL;
		}
	}
}
