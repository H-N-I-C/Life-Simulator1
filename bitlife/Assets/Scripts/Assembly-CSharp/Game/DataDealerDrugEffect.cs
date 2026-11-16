using System.Collections.Generic;

namespace Game
{
	public class DataDealerDrugEffect
	{
		private string Effect__BackingField;

		private List<string> DrugList__BackingField;

		private int GoodTripLikelihood__BackingField;

		public string Effect
		{
			get
			{
				return Effect__BackingField;
			}
			set
			{
				Effect__BackingField = value;
			}
		}

		public List<string> DrugList
		{
			get
			{
				return DrugList__BackingField;
			}
			set
			{
				DrugList__BackingField = value;
			}
		}

		public int GoodTripLikelihood
		{
			get
			{
				return GoodTripLikelihood__BackingField;
			}
			set
			{
				GoodTripLikelihood__BackingField = value;
			}
		}
	}
}
