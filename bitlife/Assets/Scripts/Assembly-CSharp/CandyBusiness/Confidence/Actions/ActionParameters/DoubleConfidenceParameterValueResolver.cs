namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class DoubleConfidenceParameterValueResolver : ConfidenceParameterValueResolver<double>
	{
		protected override double ResolveInternal(string parameterString)
		{
			return 0.0;
		}
	}
}
