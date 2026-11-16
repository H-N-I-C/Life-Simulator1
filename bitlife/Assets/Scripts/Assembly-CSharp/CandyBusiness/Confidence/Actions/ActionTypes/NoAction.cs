namespace CandyBusiness.Confidence.Actions.ActionTypes
{
	public sealed class NoAction : ConfidenceAction
	{
		public NoAction(ConfidenceActionSchema schema)
			: base(null)
		{
		}

		public override void Execute()
		{
		}
	}
}
