using System;

[Serializable]
public class SimSupplier
{
	public string Name;

	public bool IsOriginal;

	public double VolumeRequirement;

	public float Cost;

	public int Age;

	public int Quality;

	private SimSupplier()
	{
	}

	public SimSupplier(bool isOriginal, SimProduct theProduct)
	{
	}

	public SimSupplier(bool isStartup, int businessTier, int numFacilities, int numProducts, int numEmployees, int facilityEmployeeMax, SimProduct theProduct)
	{
	}

	public SimSupplier(SimProduct theProduct)
	{
	}

	public void IncrementAge()
	{
	}
}
