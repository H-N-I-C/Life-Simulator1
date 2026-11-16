using System.Collections;

public class VoltronComponent
{
	public int mFeature;

	public int mStyle;

	public string mColor;

	public ArrayList mFileEntries;

	public bool mHidden;

	public VoltronComponent(int pFeature, int pStyle, string pColor, ArrayList pFiles)
	{
	}

	public void SetStyleColorFiles(int pStyle, string pColor, ArrayList pFiles)
	{
	}

	public string GetData()
	{
		return null;
	}

	public void SyncStyleWithComponentForStyle(VoltronComponent pParentComponent, int pStyle)
	{
	}

	public int GetNumberOfStyles()
	{
		return 0;
	}

	public bool IsAvailable()
	{
		return false;
	}

	private void AssignAvailableFiles(ArrayList pFiles)
	{
	}

	public bool IsStyleNone()
	{
		return false;
	}

	public string GetFilename()
	{
		return null;
	}

	private string GetFilenameForStyle(int pStyle)
	{
		return null;
	}

	public string GetFilenameID()
	{
		return null;
	}

	public string GetFilenameIDForStyle(int pStyle)
	{
		return null;
	}
}
