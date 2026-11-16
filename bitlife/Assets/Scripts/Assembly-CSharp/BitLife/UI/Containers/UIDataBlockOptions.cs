using System.Collections.Generic;
using BitLife.UI.Interfaces;

namespace BitLife.UI.Containers
{
	public class UIDataBlockOptions
	{
		private List<IguiOptions> Options__BackingField;

		public List<IguiOptions> Options
		{
			get
			{
				return Options__BackingField;
			}
			private set
			{
				Options__BackingField = value;
			}
		}

		public UIDataBlockOptions()
		{
		}

		public UIDataBlockOptions(List<IguiOptions> options)
		{
		}

		public void Add(IguiOptions option)
		{
		}
	}
}
