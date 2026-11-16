public class WAV
{
	private float[] LeftChannel__BackingField;

	private float[] RightChannel__BackingField;

	private int ChannelCount__BackingField;

	private int SampleCount__BackingField;

	private int Frequency__BackingField;

	public float[] LeftChannel
	{
		get
		{
			return LeftChannel__BackingField;
		}
		internal set
		{
			LeftChannel__BackingField = value;
		}
	}

	public float[] RightChannel
	{
		get
		{
			return RightChannel__BackingField;
		}
		internal set
		{
			RightChannel__BackingField = value;
		}
	}

	public int ChannelCount
	{
		get
		{
			return ChannelCount__BackingField;
		}
		internal set
		{
			ChannelCount__BackingField = value;
		}
	}

	public int SampleCount
	{
		get
		{
			return SampleCount__BackingField;
		}
		internal set
		{
			SampleCount__BackingField = value;
		}
	}

	public int Frequency
	{
		get
		{
			return Frequency__BackingField;
		}
		internal set
		{
			Frequency__BackingField = value;
		}
	}

	private static float bytesToFloat(byte firstByte, byte secondByte)
	{
		return 0f;
	}

	private static int bytesToInt(byte[] bytes, int offset = 0)
	{
		return 0;
	}

	private static byte[] GetBytes(string filename)
	{
		return null;
	}

	public WAV(string filename)
	{
	}

	public WAV(byte[] wav)
	{
	}

	public override string ToString()
	{
		return null;
	}
}
