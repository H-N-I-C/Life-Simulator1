using BitLife.UI.Data;

public class CustomTheme
{
	public static readonly CustomTheme Vampire;

	public static readonly CustomTheme Valentines;

	public static readonly CustomTheme ValentinesList;

	public static readonly CustomTheme AuraRingRelationshipsList;

	public static readonly CustomTheme MovieDirectorList;

	public readonly THEME_COLOR BackgroundColor;

	public readonly THEME_COLOR PrimaryFontColor;

	public readonly THEME_COLOR SecondaryFontColor;

	public readonly THEME_COLOR? PriceFontColor;

	public readonly string CustomIcon;

	public readonly bool HideSecondaryImage;

	public readonly THEME_COLOR? RightHandImageColor;

	public readonly bool ForceShowEllipsis;

	private CustomTheme(THEME_COLOR backgroundColor, THEME_COLOR fontColor, string customIcon = "")
	{
	}

	private CustomTheme(THEME_COLOR backgroundColor, THEME_COLOR primaryFontColor, THEME_COLOR secondaryFontColor, THEME_COLOR? priceFontColor, string customIcon = "", bool hideSecondaryImage = false, THEME_COLOR? rightHandImageColor = null, bool forceShowEllipsis = false)
	{
	}
}
