namespace Game
{
	public class DataDealerCutDrugName
	{
		private string Name__BackingField;

		private string BaseDrug__BackingField;

		private int CommonnessScore__BackingField;

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

		public string BaseDrug
		{
			get
			{
				return BaseDrug__BackingField;
			}
			set
			{
				BaseDrug__BackingField = value;
			}
		}

		public int CommonnessScore
		{
			get
			{
				return CommonnessScore__BackingField;
			}
			set
			{
				CommonnessScore__BackingField = value;
			}
		}
	}
}
