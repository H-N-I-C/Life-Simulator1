namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class UIntConfidenceParameterValueResolver : ConfidenceParameterValueResolver<uint>
	{
		protected override uint ResolveInternal(string parameterString)
		{
			return 0u;
		}
	}
}
