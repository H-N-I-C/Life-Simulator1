using System.Collections.Generic;

public class SimOutdoorOffRoadingActivity
{
	public enum MadeLoveWithPassengerStatus
	{
		None = 0,
		DidMakeLove = 1,
		DidNotMakeLove = 2
	}

	private sealed class _003C_003Ec__DisplayClass20_0
	{
		public SimPerson blackListedPassenger;

		internal bool _003CRemoveBlackListPassengers_003Eb__0(SimPerson passenger)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass23_0
	{
		public RECREATIONAL_VEHICLE_TYPE[] acceptableVehicleTypes;

		internal bool _003CGetPassengerVehicle_003Eb__0(SimRecreationalVehicle x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass23_1
	{
		public SimRecreationalVehicle x;

		internal bool _003CGetPassengerVehicle_003Eb__1(RECREATIONAL_VEHICLE_TYPE acceptableType)
		{
			return false;
		}
	}

	private readonly List<SimPerson> _blackListedPassengers;

	private readonly string Location__BackingField;

	private SimPerson Passenger__BackingField;

	private readonly SimRecreationalVehicle Vehicle__BackingField;

	private MadeLoveWithPassengerStatus MadeLoveStatus__BackingField;

	public string Location => Location__BackingField;

	public SimPerson Passenger
	{
		get
		{
			return Passenger__BackingField;
		}
		set
		{
			Passenger__BackingField = value;
		}
	}

	public SimRecreationalVehicle Vehicle => Vehicle__BackingField;

	public MadeLoveWithPassengerStatus MadeLoveStatus
	{
		get
		{
			return MadeLoveStatus__BackingField;
		}
		set
		{
			MadeLoveStatus__BackingField = value;
		}
	}

	public SimOutdoorOffRoadingActivity(SimRecreationalVehicle outdoorVehicle, string location)
	{
	}

	public SimOutdoorOffRoadingActivity(SimOutdoorOffRoadingActivity other, string location = "")
	{
	}

	public string LocationDescription()
	{
		return null;
	}

	public void BlackList(SimPerson person)
	{
	}

	public void RemoveBlackListPassengers(List<SimPerson> passengerList)
	{
	}

	public void ImpactVehicleCondition(int value)
	{
	}

	public bool HasPassenger()
	{
		return false;
	}

	public SimRecreationalVehicle GetPassengerVehicle()
	{
		return null;
	}
}
