using CandyBusiness.Confidence.Actions.ActionParameters;

namespace CandyBusiness.Confidence.Actions
{
	public abstract class ConfidenceAction
	{
		private ConfidenceActionSchema Schema__BackingField;

		private ConfidenceParameter[] Parameters__BackingField;

		protected ConfidenceActionSchema Schema
		{
			get
			{
				return Schema__BackingField;
			}
			private set
			{
				Schema__BackingField = value;
			}
		}

		protected ConfidenceParameter[] Parameters
		{
			get
			{
				return Parameters__BackingField;
			}
			private set
			{
				Parameters__BackingField = value;
			}
		}

		protected ConfidenceAction(ConfidenceActionSchema schema)
		{
		}

		public abstract void Execute();

		public object[] ResolveParameterValues()
		{
			return null;
		}

		public static ConfidenceAction Create(ConfidenceActionSchema schema)
		{
			return null;
		}
	}
}
