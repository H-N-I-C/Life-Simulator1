namespace CandyBusiness.Confidence.Actions.ActionParameters
{
	public class CharConfidenceParameterValueResolver : ConfidenceParameterValueResolver<char>
	{
		protected override char ResolveInternal(string parameterString)
		{
			return '\0';
		}
	}
}
