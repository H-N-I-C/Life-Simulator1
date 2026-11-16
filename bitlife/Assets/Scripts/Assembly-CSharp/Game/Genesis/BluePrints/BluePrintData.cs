using System.Collections.Generic;
using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintData
	{
		private ScenarioController _controller;

		private int IntValue__BackingField;

		private float FloatValue__BackingField;

		private string StringValue__BackingField;

		private List<BluePrintDataKeyValue> ObjectData__BackingField;

		public int IntValue
		{
			get
			{
				return IntValue__BackingField;
			}
			set
			{
				IntValue__BackingField = value;
			}
		}

		public float FloatValue
		{
			get
			{
				return FloatValue__BackingField;
			}
			set
			{
				FloatValue__BackingField = value;
			}
		}

		public string StringValue
		{
			get
			{
				return StringValue__BackingField;
			}
			set
			{
				StringValue__BackingField = value;
			}
		}

		public List<BluePrintDataKeyValue> ObjectData
		{
			get
			{
				return ObjectData__BackingField;
			}
			set
			{
				ObjectData__BackingField = value;
			}
		}

		public ScenarioController Controller => null;

		public void SetController(ScenarioController controller = null)
		{
		}

		public string Inject(string value)
		{
			return null;
		}
	}
}
