namespace CandyBusiness.Confidence.Actions.ActionTypes
{
	public sealed class LogAction : ConfidenceAction
	{
		private string LogValue__BackingField;

		public string LogValue
		{
			get
			{
				return LogValue__BackingField;
			}
			set
			{
				LogValue__BackingField = value;
			}
		}

		public LogAction(ConfidenceActionSchema schema)
			: base(null)
		{
		}

		public override void Execute()
		{
		}
	}
}
