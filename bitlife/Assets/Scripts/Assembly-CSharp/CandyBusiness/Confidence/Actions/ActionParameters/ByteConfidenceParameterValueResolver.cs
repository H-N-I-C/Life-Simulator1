namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class ByteConfidenceParameterValueResolver : ConfidenceParameterValueResolver<byte>
	{
		protected override byte ResolveInternal(string parameterString)
		{
			return 0;
		}
	}
}
