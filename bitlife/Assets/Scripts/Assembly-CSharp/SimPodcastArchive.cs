using System;
using System.Collections.Generic;
using Genesis.SerializableLists;

[Serializable]
public class SimPodcastArchive
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SerializableMenuItem, bool> _003C_003E9__10_0;

		internal bool _003CGetListItems_003Eb__10_0(SerializableMenuItem content)
		{
			return false;
		}
	}

	private const string DefaultSeasonEmojiReference = "mic-studio";

	private const string DefaultLiveStreamEmojiReference = "users";

	private int _activePodcastIndex;

	private List<SimPodcastContent> AllContent__BackingField;

	public List<SimPodcastContent> AllContent
	{
		get
		{
			return AllContent__BackingField;
		}
		private set
		{
			AllContent__BackingField = value;
		}
	}

	public SimPodcastContent GetActiveContent()
	{
		return null;
	}

	public void SetActiveContent(int index)
	{
	}

	public void SetActiveContent(SimPodcastContent content)
	{
	}

	public static List<SerializableMenuItem> GetListItems()
	{
		return null;
	}

	private static SerializableMenuItem GetArchiveItemScenario(SimPodcastContent content)
	{
		return null;
	}
}
