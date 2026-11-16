using System;
using UnityEngine;

namespace Game.Util.Debugging
{
	public struct Logger
	{
		private const string DefineCwDebug = "CW_DEBUG";

		private const string DefineDevBuild = "DEVELOPMENT_BUILD";

		private const string DefineEditor = "UNITY_EDITOR";

		private readonly string _name;

		private readonly bool _enabled;

		private readonly Color? _color;

		private readonly string _fileName;

		public string Name => null;

		public bool Enabled => false;

		private Logger(string name, bool enabled, Color? color, string fileName)
		{
			_name = null;
			_enabled = false;
			_color = null;
			_fileName = null;
		}

		public static Logger Get(string name = null)
		{
			return default;
		}

		public Logger InitializeWith(LoggerConfig config)
		{
			return default;
		}

		public Logger SetEnabled(bool enabled)
		{
			return default;
		}

		public Logger ToFile(string fileName)
		{
			return default;
		}

		public Logger ChangeColor(Color col)
		{
			return default;
		}

		public Logger ClearColor()
		{
			return default;
		}

		public void Log(object message)
		{
		}

		public void LogWarning(object message)
		{
		}

		public void LogError(object message)
		{
		}

		public void LogException(Exception exception)
		{
		}

		private void Write(LogType level, object message)
		{
		}

		private Color ResolveColor(LogType level)
		{
			return default;
		}

		private static string LevelLabel(LogType level)
		{
			return null;
		}
	}
}
