using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioLandlordRentListing : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPropertyRemodel, DataLandlordHomeRemodelFeature> _003C_003E9__2_0;

		internal DataLandlordHomeRemodelFeature _003C_002Ector_003Eb__2_0(SimPropertyRemodel x)
		{
			return null;
		}
	}

	protected SimHouse _property;

	protected List<DataLandlordHomeRemodelFeature> _propertyFeatures;

	public ScenarioLandlordRentListing(SimHouse property)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private List<SimTenant> GenerateApplicantList(int currentOdds, float totalOdds, double rentPerMonth)
	{
		return null;
	}
}
