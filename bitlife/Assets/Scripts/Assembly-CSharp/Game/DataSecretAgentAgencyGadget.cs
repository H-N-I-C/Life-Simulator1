using System.Collections.Generic;

namespace Game
{
	public class DataSecretAgentAgencyGadget
	{
		private string Name__BackingField;

		private SECRET_AGENT_SPECIALIZATION Specialization__BackingField;

		private string Description__BackingField;

		private List<string> Uses__BackingField;

		private bool IsPlural__BackingField;

		private float MinCost__BackingField;

		private float MaxCost__BackingField;

		private int MinimumGadgetRank__BackingField;

		public string Name
		{
			get
			{
				return Name__BackingField;
			}
			set
			{
				Name__BackingField = value;
			}
		}

		public SECRET_AGENT_SPECIALIZATION Specialization
		{
			get
			{
				return Specialization__BackingField;
			}
			set
			{
				Specialization__BackingField = value;
			}
		}

		public string Description
		{
			get
			{
				return Description__BackingField;
			}
			set
			{
				Description__BackingField = value;
			}
		}

		public List<string> Uses
		{
			get
			{
				return Uses__BackingField;
			}
			set
			{
				Uses__BackingField = value;
			}
		}

		public bool IsPlural
		{
			get
			{
				return IsPlural__BackingField;
			}
			set
			{
				IsPlural__BackingField = value;
			}
		}

		public float MinCost
		{
			get
			{
				return MinCost__BackingField;
			}
			set
			{
				MinCost__BackingField = value;
			}
		}

		public float MaxCost
		{
			get
			{
				return MaxCost__BackingField;
			}
			set
			{
				MaxCost__BackingField = value;
			}
		}

		public int MinimumGadgetRank
		{
			get
			{
				return MinimumGadgetRank__BackingField;
			}
			set
			{
				MinimumGadgetRank__BackingField = value;
			}
		}
	}
}
