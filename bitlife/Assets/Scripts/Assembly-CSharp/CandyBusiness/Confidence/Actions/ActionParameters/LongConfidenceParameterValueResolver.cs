namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class LongConfidenceParameterValueResolver : ConfidenceParameterValueResolver<long>
	{
		protected override long ResolveInternal(string parameterString)
		{
			return 0L;
		}
	}
}
