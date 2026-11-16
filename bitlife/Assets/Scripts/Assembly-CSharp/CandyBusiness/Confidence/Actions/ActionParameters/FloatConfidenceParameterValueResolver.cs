namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class FloatConfidenceParameterValueResolver : ConfidenceParameterValueResolver<float>
	{
		protected override float ResolveInternal(string parameterString)
		{
			return 0f;
		}
	}
}
