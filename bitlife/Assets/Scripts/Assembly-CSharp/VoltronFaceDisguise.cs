using System.Collections;

public class VoltronFaceDisguise
{
	public const int VAMPIRE_DISGUISE_STYLE = 23;

	public int mType;

	public int mStyle;

	public string mColor;

	public VoltronFaceDisguise(int pType, int pStyle, string pColor)
	{
	}

	public bool IsColorizable()
	{
		return false;
	}

	public int GetFeatureToLayerAbove()
	{
		return 0;
	}

	public int GetFeatureToAdoptColorFrom()
	{
		return 0;
	}

	public ArrayList GetSubmissiveFeatures()
	{
		return null;
	}

	public ArrayList GetSubmissiveAccessories()
	{
		return null;
	}

	public string GetFilenameForStageGender(int pStage, int pGender)
	{
		return null;
	}

	public string GetFolderForStageGender(int pStage, int pGender)
	{
		return null;
	}

	public string GetSegmentForStageGender(int pStage, int pGender)
	{
		return null;
	}

	public bool IsEqualToDisguise(VoltronFaceDisguise pDisguise, bool ignoreColor)
	{
		return false;
	}

	public bool IsVampireDisguise()
	{
		return false;
	}

	public static string GetFilenameSegmentForType(int pType)
	{
		return null;
	}

	public static ArrayList GetRandomDisguiseSetForStageGender(int pStage, int pGender, bool overrideWithVampireDisguise)
	{
		return null;
	}

	public static ArrayList GetRandomDrugAddictDisguiseSetForStageGender(int pStage, int pGender, bool isCop, bool overrideWithVampireDisguise)
	{
		return null;
	}

	public static int GetNumberOfAvailableStylesForStageGenderType(int pStage, int pGender, int pType)
	{
		return 0;
	}

	public static bool DisguiseSetsEqual(ArrayList pSetA, ArrayList pSetB, bool pIgnoreColor)
	{
		return false;
	}

	public static VoltronFaceDisguise CreateVampireDisguise()
	{
		return null;
	}
}
