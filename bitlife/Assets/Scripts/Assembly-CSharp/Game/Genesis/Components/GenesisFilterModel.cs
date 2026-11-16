using System;

namespace Game.Genesis.Components
{
	public class GenesisFilterModel
	{
		private bool Not__BackingField;

		private MATH_OPERATOR Operator__BackingField;

		private GENESIS_FILTER Filter__BackingField;

		private string FunctionName__BackingField;

		private string Data__BackingField;

		private object[] Parameters__BackingField;

		private string[] DataSplit__BackingField;

		private string ContainerId__BackingField;

		private string RootAlias__BackingField;

		private Action SetSelectedAction__BackingField;

		public bool Not
		{
			get
			{
				return Not__BackingField;
			}
			set
			{
				Not__BackingField = value;
			}
		}

		public MATH_OPERATOR Operator
		{
			get
			{
				return Operator__BackingField;
			}
			set
			{
				Operator__BackingField = value;
			}
		}

		public GENESIS_FILTER Filter
		{
			get
			{
				return Filter__BackingField;
			}
			set
			{
				Filter__BackingField = value;
			}
		}

		public string FunctionName
		{
			get
			{
				return FunctionName__BackingField;
			}
			set
			{
				FunctionName__BackingField = value;
			}
		}

		public string Data
		{
			get
			{
				return Data__BackingField;
			}
			set
			{
				Data__BackingField = value;
			}
		}

		public object[] Parameters
		{
			get
			{
				return Parameters__BackingField;
			}
			set
			{
				Parameters__BackingField = value;
			}
		}

		public string[] DataSplit
		{
			get
			{
				return DataSplit__BackingField;
			}
			set
			{
				DataSplit__BackingField = value;
			}
		}

		public string ContainerId
		{
			get
			{
				return ContainerId__BackingField;
			}
			set
			{
				ContainerId__BackingField = value;
			}
		}

		public string RootAlias
		{
			get
			{
				return RootAlias__BackingField;
			}
			set
			{
				RootAlias__BackingField = value;
			}
		}

		public Action SetSelectedAction
		{
			get
			{
				return SetSelectedAction__BackingField;
			}
			set
			{
				SetSelectedAction__BackingField = value;
			}
		}
	}
}
