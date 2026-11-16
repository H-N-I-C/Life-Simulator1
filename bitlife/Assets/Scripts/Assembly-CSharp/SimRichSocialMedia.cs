using System;
using System.Collections;

[Serializable]
public class SimRichSocialMedia
{
	private SimSocialMediaChannel ChannelFacebook__BackingField;

	private SimSocialMediaChannel ChannelInstagram__BackingField;

	private SimSocialMediaChannel ChannelTikTok__BackingField;

	private SimSocialMediaChannel ChannelTwitter__BackingField;

	private SimSocialMediaChannel ChannelYouTube__BackingField;

	private SimSocialMediaChannel ChannelTwitch__BackingField;

	private SimSocialMediaChannel ChannelOnlyFans__BackingField;

	[NonSerialized]
	private SocialMediaAdapter _socialMediaAdapter;

	public SimSocialMediaChannel ChannelFacebook
	{
		get
		{
			return ChannelFacebook__BackingField;
		}
		set
		{
			ChannelFacebook__BackingField = value;
		}
	}

	public SimSocialMediaChannel ChannelInstagram
	{
		get
		{
			return ChannelInstagram__BackingField;
		}
		set
		{
			ChannelInstagram__BackingField = value;
		}
	}

	public SimSocialMediaChannel ChannelTikTok
	{
		get
		{
			return ChannelTikTok__BackingField;
		}
		set
		{
			ChannelTikTok__BackingField = value;
		}
	}

	public SimSocialMediaChannel ChannelTwitter
	{
		get
		{
			return ChannelTwitter__BackingField;
		}
		set
		{
			ChannelTwitter__BackingField = value;
		}
	}

	public SimSocialMediaChannel ChannelYouTube
	{
		get
		{
			return ChannelYouTube__BackingField;
		}
		set
		{
			ChannelYouTube__BackingField = value;
		}
	}

	public SimSocialMediaChannel ChannelTwitch
	{
		get
		{
			return ChannelTwitch__BackingField;
		}
		set
		{
			ChannelTwitch__BackingField = value;
		}
	}

	public SimSocialMediaChannel ChannelOnlyFans
	{
		get
		{
			return ChannelOnlyFans__BackingField;
		}
		set
		{
			ChannelOnlyFans__BackingField = value;
		}
	}

	public SocialMediaAdapter SocialMediaAdapter => null;

	public void IncrementYears()
	{
	}

	public void SignUpForAccountOnChannel(int channel)
	{
	}

	public void DeleteAccountOnChannel(int channel)
	{
	}

	public bool HeroIsOnSocialMedia()
	{
		return false;
	}

	public ArrayList GetAllJoinedChannelsArray()
	{
		return null;
	}

	public int GetTotalFollowers()
	{
		return 0;
	}

	public int GetTotalPosts()
	{
		return 0;
	}

	public bool HeroIsVerifiedOnAChannel()
	{
		return false;
	}

	public bool HeroQualifiesAsInfluencer()
	{
		return false;
	}

	public string GetEmojiForChannelWithBiggestInfluence()
	{
		return null;
	}

	public void FindAndRemoveTroll(SimSocialMediaTroll troll)
	{
	}

	public SimSocialMediaChannel GetSocialMediaChannelForChannel(int theChannel)
	{
		return null;
	}

	public static string GetNameForChannel(int channel)
	{
		return null;
	}

	public bool CanHaveAccoundSuspended()
	{
		return false;
	}
}
