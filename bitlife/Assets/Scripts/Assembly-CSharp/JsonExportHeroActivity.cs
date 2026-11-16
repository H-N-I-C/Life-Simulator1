using System;

public class JsonExportHeroActivity
{
	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public JsonExportHeroActivity _003C_003E4__this;

		public string code;

		internal void _003CProcessCode_003Eb__0()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass17_0
	{
		public Action callback;

		public JsonExportHeroActivity _003C_003E4__this;

		internal void _003CDownloadAndSerializeFriend_003Eb__0(string result)
		{
		}
	}

	private SimPerson Friend__BackingField;

	private bool Uploaded__BackingField;

	private bool Error__BackingField;

	private string _serializedHero;

	public SimPerson Friend
	{
		get
		{
			return Friend__BackingField;
		}
		private set
		{
			Friend__BackingField = value;
		}
	}

	public bool Uploaded
	{
		get
		{
			return Uploaded__BackingField;
		}
		private set
		{
			Uploaded__BackingField = value;
		}
	}

	public bool Error
	{
		get
		{
			return Error__BackingField;
		}
		private set
		{
			Error__BackingField = value;
		}
	}

	public static string GetQrCodeContent()
	{
		return null;
	}

	private static bool IsIdImported(string lifeId)
	{
		return false;
	}

	private static bool IsHeroId(string lifeId)
	{
		return false;
	}

	public void ProcessCode(string code)
	{
	}

	private void DownloadAndSerializeFriend(string id, Action callback)
	{
	}

	public void SerializeAndUploadHero()
	{
	}

	private void StartUpload()
	{
	}

	private static string GetFileId()
	{
		return null;
	}

	private void _003CStartUpload_003Eb__19_0(bool result)
	{
	}
}
