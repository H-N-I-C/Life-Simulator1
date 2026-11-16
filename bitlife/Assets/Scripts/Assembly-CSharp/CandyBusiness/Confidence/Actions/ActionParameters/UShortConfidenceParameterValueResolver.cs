namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class UShortConfidenceParameterValueResolver : ConfidenceParameterValueResolver<ushort>
	{
		protected override ushort ResolveInternal(string parameterString)
		{
			return 0;
		}
	}
}
