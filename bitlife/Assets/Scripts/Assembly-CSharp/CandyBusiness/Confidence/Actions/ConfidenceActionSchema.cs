using System;

namespace CandyBusiness.Confidence.Actions
{
	[Serializable]
	public class ConfidenceActionSchema
	{
		public ConfidenceActionType ActionTypeType;

		public string RawActionValue;

		public ConfidenceActionParameterSchema[] Parameters;
	}
}
